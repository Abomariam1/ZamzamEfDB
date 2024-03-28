using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Entites;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.ReturnPurchases.Commands.Create;
public record ReturnePurchaseCreateCommand: IRequest<Result<ReturnePurchaseResponse>>
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

internal class ReturnePurchaseCreateCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<ReturnePurchaseCreateCommand, Result<ReturnePurchaseResponse>>
{
    public async Task<Result<ReturnePurchaseResponse>> Handle(ReturnePurchaseCreateCommand request, CancellationToken cancellationToken)
    {
        /*التحقق من اختيار فاتورة مشتريات*/
        PurchaseOrder? purchase = unitOfWork.Repository<PurchaseOrder>().Entities
            .Include(x => x.OrderDetails)
            .SingleOrDefault(x => x.Id == request.PurchaseId && x.IsDeleted == false);
        if(purchase == null) return await Result<ReturnePurchaseResponse>
                .FailureAsync("يجب اختيار فاتورة مشتريات");

        /*التحقق من وجود اصناف بالفاتورة*/
        if(request.Details == null) return await Result<ReturnePurchaseResponse>
                .FailureAsync("يجب اضافة اصناف للفاتورة");

        /*التحقق من وجود اصناف متواجدة بفاتورة المشتريات المختارة وليست اصناف اخرى*/
        bool passed = true;
        request.Details.ForEach(x =>
        {
            OrderDetail? detail = purchase.OrderDetails
            .Where(s => s.OrderId == purchase.Id)
            .FirstOrDefault(s => s.ItemId == x.ItemId && s.IsDeleted == false);
            if(detail == null)
                passed = false;
        });
        if(!passed) return await Result<ReturnePurchaseResponse>.FailureAsync("الصنف غير متواجد في فاتورة المشتريات");

        //---------------------------------------
        List<OrderDetail> details = [];
        List<Item> items = [];
        List<ItemOperation> itemOperations = [];
        Supplier? supp = await unitOfWork.Repository<Supplier>().GetByIdAsync(request.SupplierId);
        foreach(ODetails requestDetails in request.Details!)
        {
            Item? product = await unitOfWork.Repository<Item>().GetByIdAsync(requestDetails.ItemId);
            if(product == null)
                return await Result<ReturnePurchaseResponse>.FailureAsync($"لم يتم العثور على صنف");
            OrderDetail? detail = new()
            {
                Item = product,
                Price = requestDetails.Price,
                Quantity = requestDetails.Quantity,
                Discount = requestDetails.Discount,
                CreatedBy = request.CreatedBy
            };

            ItemOperation itmOp = new()
            {
                ItemId = product.Id,
                Value = requestDetails.Quantity,
                OldBalance = product.Balance,
                NewBalance = product.Balance - requestDetails.Quantity,
                OperationType = OperationType.Credit,
                OrderType = OrderType.PurchasReturns,
            };

            product.Balance -= requestDetails.Quantity;
            product.UpdatedDate = DateTime.UtcNow;
            product.UpdatedBy = request.CreatedBy;
            items.Add(product);
            itemOperations.Add(itmOp);
            details.Add(detail);
        }
        /*-------------------------تحميل فاتورة مرتجعات المشتريات-----------------------------------*/
        ReturnPurchaseOrder rePurchase = new()
        {
            Purchase = purchase,
            Supplier = supp,
            InvoiceNumber = request.SuppInvID,
            ReasonForReturn = request.ResonForReturn,
            OrderDate = request.OrderDate,
            TotalPrice = request.TotalPrice,
            TotalDiscount = request.TotalDiscount,
            TotalPayed = request.TotalPayed,
            TotalRemaining = request.TotalRemaining,
            OrderType = OrderType.PurchasReturns,
            InvoiceType = request.InvoiceType == 0 ? InvoiceType.Cash : InvoiceType.Installment,
            EmployeeId = request.EmployeeId,
            OrderDetails = [.. details],
            CreatedBy = request.CreatedBy,
        };
        ReturnPurchaseOrder? rOrder = await unitOfWork.Repository<ReturnPurchaseOrder>().AddAsync(rePurchase);
        /*---------------------حفظ عملية مرتجع مشتريات في جدول عمليات مرتجع مشتريات من مورد------------------------------------*/
        SupplierOperations? supOperatin = new()
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
        SupplierOperations? op = await unitOfWork.Repository<SupplierOperations>().AddAsync(supOperatin);
        /*---------------------------------------------------------------*/
        /*------------اثبات عملية البيع بالاجل وتحميلها للمورد--------------*/
        if(request.InvoiceType == 1)
        {
            if(supp != null)
            {
                supp.Balance = supOperatin.NewBalance;
                Supplier? updatedSupp = await unitOfWork.Repository<Supplier>().UpdateAsync(supp);
            }
        }
        /*---------------------------------------------------------------*/
        rOrder.AddDomainEvent(new ReturnPurchasesCreatedEvent(rOrder));
        items.ForEach(x => unitOfWork.Repository<Item>().UpdateAsync(x));

        itemOperations.ForEach(x =>
        {
            x.Order = rOrder;
            unitOfWork.Repository<ItemOperation>().AddAsync(x);
        });
        int count = await unitOfWork.Save(cancellationToken);

        return count > 0 ? await Result<ReturnePurchaseResponse>.SuccessAsync(new ReturnePurchaseResponse(), "تم انشاء فاتورة مرتجعات مشتريات بنجاح")
            : await Result<ReturnePurchaseResponse>.FailureAsync("فشل في انشاء فاتورة مرتجعات مشتريات");
    }
}
