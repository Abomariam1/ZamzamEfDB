using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Features.Customers.Commands.Create;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Entites;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Sales.Commands.Create
{
    public record SaleCreateCommand : IRequest<Result<int>>, IMapFrom<SaleOrder>
    {
        public int CustomerId { get; set; } = 0;
        public string CustomerName { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public int AreaId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Payed { get; set; }
        public decimal Remains { get; set; }
        public OrderType OrderType { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int EmployeeId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public string CreatedBy { get; set; }
    }

    internal class SaleCreateCommandHandler : IRequestHandler<SaleCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaleCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                        Name = request.CustomerName,
                        Phone = request.Phone,
                        Address = request.Address,
                        NationalCardId = request.NationalCardId,
                        Notes = request.Notes,
                        AreaId = request.AreaId
                    };
                    cust = await _unitOfWork.Repository<Customer>().AddAsync(newCust);
                    cust.AddDomainEvent(new CreatedCustomerEvent(cust));
                }
                else
                {
                    cust.Name = request.CustomerName;
                    cust.Phone = request.Phone;
                    cust.Address = request.Address;
                    cust.NationalCardId = request.NationalCardId;
                    cust.Notes = request.Notes;
                    cust.AreaId = request.AreaId;
                }

                SaleOrder order = new()
                {
                    CustomerId = cust.Id,
                    OrderDate = request.OrderDate,
                    TotalPrice = request.TotalPrice,
                    TotalDiscount = request.TotalDiscount,
                    NetPrice = request.NetPrice,
                    Payed = request.Payed,
                    Remains = request.Remains,
                    OrderType = request.OrderType,
                    InvoiceType = request.InvoiceType,
                    EmployeeId = request.EmployeeId,
                    OrderDetails = request.OrderDetails,
                    CreatedBy = request.CreatedBy,
                };
                Order? addedOrder = await _unitOfWork.Repository<Order>().AddAsync(order);
                cust.AddDomainEvent(new CreatedCustomerEvent(cust));
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
