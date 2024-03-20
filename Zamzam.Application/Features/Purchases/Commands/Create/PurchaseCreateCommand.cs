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
        public decimal TotalRemained { get; set; } // اجمالي المتبقي
        public InvoiceType InvoiceType { get; set; }
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
            List<OrderDetail> details = [];
            List<Item> items = [];
            /*----------------التحقق من عدم تكرار فاتورة المورد----------------------------------*/
            var inv = await _unitOfWork.Repository<PurchaseOrder>().GetByNameAsync(x => x.InvoiceNumber == request.SuppInvID);
            if(inv != null) return await Result<PurchaseDto>.FailureAsync("هذه الفاتورة مسجلة من قبل");
            /*---------------------------------------------------------------*/

            /*----------------تحميل تفاصيل الاصناف المباعة--------------------------------*/

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
                product.Balance += requstDetails.Quantity;
                items.Add(product);
                details.Add(detail);
            }
            /*---------------------------------------------------------------*/

            /*-------------------------تحميل فاتورة المشتريات-----------------------------------*/
            PurchaseOrder purchase = new()
            {
                SupplierId = request.SupplierId,
                InvoiceNumber = request.SuppInvID,
                OrderDate = request.OrderDate,
                InvoiceType = request.InvoiceType,
                TotalPrice = request.TotalPrice,
                TotalDiscount = request.TotalDiscount,
                TotalPayed = request.TotalPayed,
                TotalRemained = request.TotalRemained,
                OrderType = OrderType.Purchase,
                OrderDetails = details,
                EmployeeId = request.EmployeeId,
                CreatedBy = request.CreatedBy,
            };
            PurchaseOrder? order = await _unitOfWork.Repository<PurchaseOrder>().AddAsync(purchase);
            /*---------------------------------------------------------------*/



            var supp = await _unitOfWork.Repository<Supplier>().GetByIdAsync(request.SupplierId);
            /*---------------------حفظ عملية شراء في جدول عمليات شراء من مورد------------------------------------*/
            SupplierOperations? operatin = new()
            {
                Order = order,
                Supplier = supp,
                OperationDate = DateTime.Now,
                OperationType = order.InvoiceType == InvoiceType.Installment ? OperationType.Credit : OperationType.None,
                CreatedBy = order.CreatedBy,
                Value = order.TotalRemained,
                OldBalance = supp.Balance,
                NewBalance = supp.Balance + order.TotalRemained,
            };
            /*---------------------------------------------------------------*/

            /*------------اثبات عملية البيع بالاجل وتحميلها للمورد--------------*/
            if(request.InvoiceType == InvoiceType.Installment)
            {
                if(supp != null)
                {
                    supp.Balance = operatin.NewBalance;
                    var updatedSupp = await _unitOfWork.Repository<Supplier>().UpdateAsync(supp);
                }
            }
            var op = await _unitOfWork.Repository<SupplierOperations>().AddAsync(operatin);
            /*---------------------------------------------------------------*/
            int count = await _unitOfWork.Save(cancellationToken);
            order.AddDomainEvent(new PurchaseCreatedEvent(purchase));
            if(count > 0)
            {
                items.ForEach(x => _unitOfWork.Repository<Item>().UpdateAsync(x));
                int itemsAdded = await _unitOfWork.Save(cancellationToken);
            }

            return count > 0 ? await Result<PurchaseDto>.SuccessAsync((PurchaseDto)purchase, "تم اضافة فاتورة الشراء بنجاح")
                : await Result<PurchaseDto>.FailureAsync("فشل في انشاء فاتورة شراء");
        }
    }
}
