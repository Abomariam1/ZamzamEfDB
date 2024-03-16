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
    public record SaleCreateCommand : IRequest<Result<int>>
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
        public int EmployeeId { get; set; }
        public ICollection<ODetails>? OrderDetails { get; set; }
        public string? CreatedBy { get; set; }
    }
    internal class SaleCreateCommandHandler : IRequestHandler<SaleCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleCreateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(SaleCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Customer? cust = await _unitOfWork.Repository<Customer>().GetByIdAsync(request.CustomerId);
                if (cust == null)
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
                    cust = await _unitOfWork.Repository<Customer>().AddAsync(newCust);
                    cust.AddDomainEvent(new CreatedCustomerEvent(cust));
                }
                //Employee? emp = await unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId);
                var ordt = new List<OrderDetail>();
                foreach (ODetails orderDetails in request.OrderDetails)
                {
                    Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(orderDetails.ItemId);
                    ordt.Add(new()
                    {
                        Item = item,
                        Quantity = orderDetails.Quantity,
                        Price = orderDetails.Price,
                        Discount = orderDetails.Discount,
                        CreatedBy = orderDetails.CreatedBy,
                    });
                    item.Balance -= orderDetails.Quantity;
                }
                SaleOrder saleOrder = new()
                {
                    OrderDate = request.OrderDate,
                    TotalPrice = request.TotalPrice,
                    TotalDiscount = request.TotalDiscount,
                    OrderType = OrderType.Sell,
                    InvoiceType = InvoiceType.Cash,
                    EmployeeId = request.EmployeeId,
                    Customer = cust,
                    OrderDetails = ordt,
                    CreatedBy = request.CreatedBy,
                };

                saleOrder.OrderDetails = ordt;
                Order? addedOrder = await _unitOfWork.SaleOrderRepository().AddAsync(saleOrder);
                saleOrder.AddDomainEvent(new SaleCreatedEvent(saleOrder));
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception e)
            {
                return await Result<int>.FailureAsync(e.Message);
            }


            return await Result<int>.SuccessAsync(0, "تم انشاء الفاتورة ....");
        }
    }
}
