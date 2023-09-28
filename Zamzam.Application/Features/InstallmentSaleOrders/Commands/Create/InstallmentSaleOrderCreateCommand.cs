using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.Customers.Commands.Create;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.InstallmentSaleOrders.Commands.Create
{
    public record InstallmentSaleOrderCreateCommand : IRequest<Result<int>>
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
        public decimal Payed { get; set; }
        public decimal Remain { get; set; }
        public decimal InstallmentValue { get; set; }
        public int InstallmentPeriodInMonths { get; set; }
        public int EmployeeId { get; set; }
        public ICollection<ODetails>? OrderDetails { get; set; }
        public string? CreatedBy { get; set; }
    }

    internal class InstallmentSaleCreateCommandHandler : IRequestHandler<InstallmentSaleOrderCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstallmentSaleCreateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(InstallmentSaleOrderCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Customer? cust = await _unitOfWork.Repository<Customer>().GetByIdAsync(request.CustomerId);
                if (cust == null)
                {
                    var newCust = new Customer()
                    {
                        Name = request.CustomerName,
                        Phone = request.Phone,
                        Address = request.Address,
                        NationalCardId = request.NationalCardId,
                        Notes = request.Notes,
                        AreaId = request.AreaId,
                        CreatedBy = request.CreatedBy,
                    };
                    cust = await _unitOfWork.Repository<Customer>().AddAsync(newCust);
                    cust.AddDomainEvent(new CreatedCustomerEvent(cust));
                }
                Employee? emp = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId);
                InstallmentedSaleOrder instlmentSaleOrder = new()
                {
                    OrderDate = request.OrderDate,
                    TotalPrice = request.TotalPrice,
                    TotalDiscount = request.TotalDiscount,
                    OrderType = OrderType.Sell,
                    InvoiceType = InvoiceType.Installment,
                    EmployeeId = emp.Id,
                    Payed = request.Payed,
                    Remains = request.Remain,
                    InstallmentValue = request.InstallmentValue,
                    InstallmentPeriodInMonths = request.InstallmentPeriodInMonths,
                    CustomerId = cust.Id,
                    CreatedBy = request.CreatedBy,
                };

                var ordt = new List<OrderDetail>();
                foreach (var item in request.OrderDetails)
                {
                    Item? itm = await _unitOfWork.Repository<Item>().GetByIdAsync(item.ItemId);
                    if (itm != null)
                    {
                        if (itm.Balance < item.Quantity || itm.Balance == 0)
                            return Result<int>.Failure(itm.Balance, "لا توجد كمية كافية في المخزن");
                        itm.Balance -= item.Quantity;
                    }
                    else
                        return Result<int>.Failure("لم يتم العثور على الصنف");
                    ordt.Add(new()
                    {
                        OrderId = instlmentSaleOrder.Id,
                        ItemId = item.ItemId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Discount = item.Discount,
                        CreatedBy = item.CreatedBy,
                    });
                    await _unitOfWork.Repository<Item>().UpdateAsync(itm);
                }
                instlmentSaleOrder.OrderDetails = ordt;
                var addedOrder = await _unitOfWork.Repository<Order>().AddAsync(instlmentSaleOrder);
                instlmentSaleOrder.AddDomainEvent(new InstalmentOrderCreatedEvent(instlmentSaleOrder));
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception e)
            {
                return await Result<int>.FailureAsync(e.Message);
            }
            return await Result<int>.SuccessAsync();
        }
    }
}
