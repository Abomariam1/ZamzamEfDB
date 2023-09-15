using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Customers.Commands.Delete
{
    public record DeleteCustomerCommand : IRequest<Result<int>>, IMapFrom<Customer>
    {
        public int Id { get; }
        public DeleteCustomerCommand()
        {
        }

        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }
    }
    internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return await Result<int>.FailureAsync("Customer cannot to be deleted");
            }
            Customer customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(request.Id);
            if (customer == null)
            {
                return await Result<int>.FailureAsync("Customer not found");
            }
            else
            {
                await _unitOfWork.Repository<Customer>().DeleteAsync(customer.Id);
                customer.AddDomainEvent(new DeleteCustomerEvent(customer));
                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(customer.Id, "Customer deleted...");
            }
        }
    }
}