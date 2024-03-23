using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Entites;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Purchases.Commands.Create
{
    public record PurchaseCreateCommand: IRequest<Result<PurchaseDto>>
    {
        public int SuppInvID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalPayed { get; set; } // اجمالي المدفوع
        public decimal TotalRemaining { get; set; } // اجمالي المتبقي
        public int InvoiceType { get; set; }
        public int EmployeeId { get; set; }
        public int SupplierId { get; set; }
        public List<ODetails>? Details { get; set; }
        public string? CreatedBy { get; set; }
    }

    internal record PurchaseCreateCommandHndler: IRequestHandler<PurchaseCreateCommand, Result<PurchaseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseCreateCommandHndler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PurchaseDto>> Handle(PurchaseCreateCommand request, CancellationToken cancellationToken)
        {

            /*----------------التحقق من عدم تكرار فاتورة المورد----------------------------------*/
            var inv = await _unitOfWork.Repository<PurchaseOrder>().GetByNameAsync(x => x.InvoiceNumber == request.SuppInvID);
            if(inv != null) return await Result<PurchaseDto>.FailureAsync("هذه الفاتورة مسجلة من قبل");
            /*---------------------------------------------------------------*/

            /****************التاكد من وجود اصناف في الفاتورة****************************/
            if(request.Details.Count <= 0) return await Result<PurchaseDto>.FailureAsync("لا توجد اصناف في الفاتورة");
            /*---------------------------------------------       --------------------------*/

            /*-----------------------التحقق من ان الفاتورة كاش وليس هناك باقي-------------------------*/
            if(request.InvoiceType == 0 && request.TotalRemaining > 0)
                return await Result<PurchaseDto>.FailureAsync("الفاتورة كاش لا يجب ان يكون هناك باقي");
            /*------------------------------------------------------------------------------*/

            /*--------------------------تحميل تفاصيل الاصناف المشتراة--------------------------------*/
            List<OrderDetail> details = [];
            List<Item> items = [];
            List<ItemOperation> operations = [];
            foreach(ODetails requstDetails in request.Details!)
            {
                Item? product = await _unitOfWork.Repository<Item>().GetByIdAsync(requstDetails.ItemId);
                if(product == null)
                    return await Result<PurchaseDto>.FailureAsync($"لم يتم العثور على صنف");
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
                    NewBalance = product.Balance + requstDetails.Quantity,
                    OperationType = OperationType.Debit,
                    OrderType = OrderType.Purchase,
                };

                product.Balance += requstDetails.Quantity;
                product.UpdatedDate = DateTime.UtcNow;
                product.UpdatedBy = request.CreatedBy;
                items.Add(product);
                operations.Add(itmOp);
                details.Add(detail);
            }
            /*---------------------------------------------------------------*/

            /*-------------------------تحميل فاتورة المشتريات-----------------------------------*/
            PurchaseOrder purchase = new()
            {
                SupplierId = request.SupplierId,
                InvoiceNumber = request.SuppInvID,
                OrderDate = request.OrderDate,
                InvoiceType = request.InvoiceType == 0 ? InvoiceType.Cash : InvoiceType.Installment,
                TotalPrice = request.TotalPrice,
                TotalDiscount = request.TotalDiscount,
                TotalPayed = request.TotalPayed,
                TotalRemaining = request.TotalRemaining,
                OrderType = OrderType.Purchase,
                OrderDetails = details,
                EmployeeId = request.EmployeeId,
                CreatedBy = request.CreatedBy,
            };
            PurchaseOrder? order = await _unitOfWork.Repository<PurchaseOrder>().AddAsync(purchase);
            /*---------------------------------------------------------------*/

            /*---------------------حفظ عملية شراء في جدول عمليات شراء من مورد------------------------------------*/
            Supplier? supp = await _unitOfWork.Repository<Supplier>().GetByIdAsync(request.SupplierId);
            SupplierOperations? operatin = new()
            {
                Order = order,
                Supplier = supp,
                OperationDate = DateTime.Now,
                OperationType = order.InvoiceType == InvoiceType.Installment ? OperationType.Credit : OperationType.Debit,
                CreatedBy = order.CreatedBy,
                Value = order.TotalRemaining,
                OldBalance = supp.Balance,
                NewBalance = supp.Balance + order.TotalRemaining,
            };
            /*---------------------------------------------------------------*/

            /*------------اثبات عملية البيع بالاجل وتحميلها للمورد--------------*/
            if(request.InvoiceType == 1)
            {
                if(supp != null)
                {
                    supp.Balance = operatin.NewBalance;
                    Supplier? updatedSupp = await _unitOfWork.Repository<Supplier>().UpdateAsync(supp);
                }
            }
            SupplierOperations? op = await _unitOfWork.Repository<SupplierOperations>().AddAsync(operatin);
            /*---------------------------------------------------------------*/

            order.AddDomainEvent(new PurchaseCreatedEvent(purchase));
            items.ForEach(x =>
            {
                x.UpdatedBy = request.CreatedBy;
                x.UpdatedDate = DateTime.Now;
                _unitOfWork.Repository<Item>().UpdateAsync(x);
            });

            operations.ForEach(x =>
            {
                x.Order = order;
                x.CreatedBy = request.CreatedBy;
                x.CreatedDate = DateTime.Now;
                _unitOfWork.Repository<ItemOperation>().AddAsync(x);
            }
            );
            int count = await _unitOfWork.Save(cancellationToken);


            return count > 0 ? await Result<PurchaseDto>.SuccessAsync((PurchaseDto)purchase, "تم اضافة فاتورة الشراء بنجاح")
                : await Result<PurchaseDto>.FailureAsync("فشل في انشاء فاتورة شراء");
        }
    }
}
