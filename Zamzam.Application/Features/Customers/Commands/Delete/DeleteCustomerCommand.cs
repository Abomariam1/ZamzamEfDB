using MediatR;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Customers.Commands.Delete
{
    public record DeleteCustomerCommand : IRequest<Result<int>>
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

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                return await Result<int>.FailureAsync("لم يتم ايجاد العميل");
            }
            else
            {
                await _unitOfWork.Repository<Customer>().DeleteAsync(customer.Id);
                int count = await _unitOfWork.Save(cancellationToken);
                if (count > 0)
                {
                    customer.AddDomainEvent(new DeleteCustomerEvent(customer));
                    return await Result<int>.SuccessAsync(customer.Id, "Customer deleted...");
                }
                return await Result<int>.FailureAsync("لم يتم ايجاد العميل");
            }
        }
    }
}