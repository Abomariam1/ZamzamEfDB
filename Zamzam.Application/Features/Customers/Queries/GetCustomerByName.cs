using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Customers.Queries;

public record GetCustomerByName(string name) : IRequest<Result<CustomerDto>>;

internal class GetCustomerByNameHandler : IRequestHandler<GetCustomerByName, Result<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerByNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CustomerDto>> Handle(GetCustomerByName request, CancellationToken cancellationToken)
    {
        var cust = await _unitOfWork.Repository<Customer>().GetByNameAsync(x => x.Name == request.name);
        if (cust != null)
        {
            return await Result<CustomerDto>.SuccessAsync((CustomerDto)cust);
        }
        return await Result<CustomerDto>.FailureAsync($"لا يوجد عميل بهذا الاسم");
    }
}
