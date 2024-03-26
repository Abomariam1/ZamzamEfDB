using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Entites;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.ReturnPurchases.Commands.Create;
public record ReturnePurchaseCreateCommand: IRequest<Result<int>>
{
    public int PurchaseId { get; set; }
    public int SuppInvID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalPayed { get; set; } // اجمالي المدفوع
    public decimal TotalRemaining { get; set; } // اجمالي المتبقي
    public int InvoiceType { get; set; }
    public string ResonForReturn { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public int SupplierId { get; set; }
    public List<ODetails>? Details { get; set; }
    public string? CreatedBy { get; set; }

}

internal class ReturnePurchaseCreateCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<ReturnePurchaseCreateCommand, Result<int>>
{
    public async Task<Result<int>> Handle(ReturnePurchaseCreateCommand request, CancellationToken cancellationToken)
    {
        /*التحقق من اختيار فاتورة مشتريات*/
        PurchaseOrder? purchase = await unitOfWork.Repository<PurchaseOrder>().GetByIdAsync(request.PurchaseId);
        if(purchase == null) return await Result<int>.FailureAsync("يجب اختيار فاتورة مشتريات");

        /*التحقق من وجود اصناف بالفاتورة*/
        if(request.Details == null) return await Result<int>.FailureAsync("يجب اضافة اصناف للفاتورة");

        /*التحقق من وجود اصناف متواجدة بفاتورة المشتريات المختارة وليست اصناف اخرى*/
        bool passed = true;
        request.Details.ForEach(x =>
        {
            OrderDetail? detail = purchase.OrderDetails.Where(s => s.OrderId == x.OrderId)
            .FirstOrDefault(s => s.ItemId == x.ItemId && s.IsDeleted == false);
            if(detail == null)
                passed = false;
        });
        if(!passed) return await Result<int>.FailureAsync("الصنف غير متواجد في فاتورة المشتريات");

        //---------------------------------------
        List<OrderDetail> details = [];
        List<Item> items = [];
        List<ItemOperation> operations = [];
        foreach(ODetails requstDetails in request.Details!)
        {
            Item? product = await unitOfWork.Repository<Item>().GetByIdAsync(requstDetails.ItemId);
            if(product == null)
                return await Result<int>.FailureAsync($"لم يتم العثور على صنف");
            OrderDetail? detail = new()
            {
                ItemId = requstDetails.ItemId,
                Price = requstDetails.Price,
                Quantity = requstDetails.Quantity,
                Discount = requstDetails.Discount,
                CreatedBy = requstDetails.CreatedBy
            };

            ItemOperation itmOp = new()
            {
                ItemId = product.Id,
                Value = requstDetails.Quantity,
                OldBalance = product.Balance,
                NewBalance = product.Balance - requstDetails.Quantity,
                OperationType = OperationType.Credit,
                OrderType = OrderType.PurchasReturns,
            };

            product.Balance -= requstDetails.Quantity;
            product.UpdatedDate = DateTime.UtcNow;
            product.UpdatedBy = request.CreatedBy;
            items.Add(product);
            operations.Add(itmOp);
            details.Add(detail);
        }
        /*-------------------------تحميل فاتورة مرتجعات المشتريات-----------------------------------*/
        ReturnPurchaseOrder rePurchase = new()
        {
            SupplierId = request.SupplierId,
            InvoiceNumber = request.SuppInvID,
            OrderDate = request.OrderDate,
            InvoiceType = request.InvoiceType == 0 ? InvoiceType.Cash : InvoiceType.Installment,
            TotalPrice = request.TotalPrice,
            TotalDiscount = request.TotalDiscount,
            TotalPayed = request.TotalPayed,
            TotalRemaining = request.TotalRemaining,
            OrderType = OrderType.PurchasReturns,
            OrderDetails = details,
            ReasonForReturn = request.ResonForReturn,
            EmployeeId = request.EmployeeId,
            CreatedBy = request.CreatedBy,
        };
        ReturnPurchaseOrder? rOrder = await unitOfWork.Repository<ReturnPurchaseOrder>().AddAsync(rePurchase);

        /*---------------------حفظ عملية شراء في جدول عمليات شراء من مورد------------------------------------*/
        Supplier? supp = await unitOfWork.Repository<Supplier>().GetByIdAsync(request.SupplierId);
        SupplierOperations? operatin = new()
        {
            Order = rOrder,
            Supplier = supp,
            OperationDate = DateTime.Now,
            OperationType = rOrder.InvoiceType == InvoiceType.Installment ? OperationType.Credit : OperationType.Debit,
            CreatedBy = rOrder.CreatedBy,
            Value = rOrder.TotalRemaining,
            OldBalance = supp.Balance,
            NewBalance = supp.Balance - rOrder.TotalRemaining,
            CreatedDate = DateTime.Now,
        };
        SupplierOperations? op = await unitOfWork.Repository<SupplierOperations>().AddAsync(operatin);
        /*---------------------------------------------------------------*/
        /*------------اثبات عملية البيع بالاجل وتحميلها للمورد--------------*/
        if(request.InvoiceType == 1)
        {
            if(supp != null)
            {
                supp.Balance = operatin.NewBalance;
                Supplier? updatedSupp = await unitOfWork.Repository<Supplier>().UpdateAsync(supp);
            }
        }
        /*---------------------------------------------------------------*/
        rOrder.AddDomainEvent(new ReturnPurchasesCreatedEvent(rOrder));
        items.ForEach(x => unitOfWork.Repository<Item>().UpdateAsync(x));

        operations.ForEach(x => unitOfWork.Repository<ItemOperation>().AddAsync(x));
        int count = await unitOfWork.Save(cancellationToken);

        return count > 0 ? await Result<int>.SuccessAsync("تم انشاء فاتورة مرتجعات مشتريات بنجاح")
            : await Result<int>.FailureAsync("فشل في انشاء فاتورة مرتجعات مشتريات");
    }
}
