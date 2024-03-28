using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.Customers.Commands.Create;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Entites;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Sales.Commands.Create
{
    public record SaleCreateCommand: IRequest<Result<SaleResponse>>
    {
        public int CustomerId { get; set; } = 0;
        public string? CustomerName { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public int AreaId { get; set; } = 0;
        public DateTime OrderDate { get; set; } = DateTime.Now.Date;
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalPayed { get; set; } // اجمالي المدفوع
        public decimal TotalRemaining { get; set; } // اجمالي المتبقي
        public int InvoiceType { get; set; }
        public int EmployeeId { get; set; }
        public ICollection<ODetails>? OrderDetails { get; set; }
        public string? CreatedBy { get; set; }
    }
    internal class SaleCreateCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<SaleCreateCommand, Result<SaleResponse>>
    {
        public async Task<Result<SaleResponse>> Handle(SaleCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Customer? cust = await unitOfWork.Repository<Customer>().GetByIdAsync(request.CustomerId);
                if(cust == null)
                {
                    var newCust = new Customer()
                    {
                        Name = request.CustomerName!,
                        Phone = request.Phone,
                        Address = request.Address,
                        NationalCardId = request.NationalCardId,
                        Notes = request.Notes,
                        AreaId = request.AreaId
                    };

                    cust = await unitOfWork.Repository<Customer>().AddAsync(newCust);
                    cust.AddDomainEvent(new CreatedCustomerEvent(cust));
                }

                List<OrderDetail> details = [];
                List<Item> items = [];
                List<ItemOperation> itemOperations = [];
                if(request.OrderDetails == null)
                    return await Result<SaleResponse>.FailureAsync("يجب اضافة اصناف للفاتورة اولا!!!!");

                foreach(ODetails requestDetails in request.OrderDetails)
                {
                    Item? product = await unitOfWork.Repository<Item>().GetByIdAsync(requestDetails.ItemId);
                    details.Add(new()
                    {
                        Item = product,
                        Quantity = requestDetails.Quantity,
                        Price = requestDetails.Price,
                        Discount = requestDetails.Discount,
                        CreatedBy = requestDetails.CreatedBy,
                    });

                    ItemOperation itmOp = new()
                    {
                        Item = product,
                        Value = requestDetails.Quantity,
                        OldBalance = product.Balance,
                        NewBalance = product.Balance - requestDetails.Quantity,
                        OperationType = OperationType.Credit,
                        OrderType = OrderType.Sell,
                    };
                    product.Balance -= requestDetails.Quantity;
                    product.UpdatedDate = DateTime.UtcNow;
                    product.UpdatedBy = request.CreatedBy;
                    items.Add(product);
                    itemOperations.Add(itmOp);
                }

                SaleOrder saleOrder = new()
                {
                    OrderDate = request.OrderDate,
                    TotalPrice = request.TotalPrice,
                    TotalDiscount = request.TotalDiscount,
                    TotalPayed = request.TotalPayed,
                    TotalRemaining = request.TotalRemaining,
                    OrderType = OrderType.Sell,
                    InvoiceType = request.InvoiceType == 0 ? InvoiceType.Cash : InvoiceType.Installment,
                    EmployeeId = request.EmployeeId,
                    Customer = cust,
                    OrderDetails = details,
                    CreatedBy = request.CreatedBy,
                };

                saleOrder.OrderDetails = [.. details];
                Order? addedOrder = await unitOfWork.Repository<SaleOrder>().AddAsync(saleOrder);
                addedOrder.AddDomainEvent(new SaleCreatedEvent(saleOrder));
                items.ForEach(i =>
                {
                    unitOfWork.Repository<Item>().UpdateAsync(i);
                });
                itemOperations.ForEach(o =>
                {
                    o.Order = addedOrder;
                    unitOfWork.Repository<ItemOperation>().AddAsync(o);
                });
                int count = await unitOfWork.Save(cancellationToken);
                return count > 0 ? await Result<SaleResponse>.SuccessAsync(new SaleResponse(), "تم انشاء الفاتورة ....")
                    : await Result<SaleResponse>.FailureAsync("فشل في انشاء فاتورة مبيعات!!!");
            }
            catch(Exception e)
            {
                return await Result<SaleResponse>.FailureAsync(e.Message);
            }
        }
    }
}
