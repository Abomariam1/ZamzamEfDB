using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Customers.Queries;

public record GetAllCustmersQuery : IRequest<Result<List<CustomerDto>>>;
internal class GetAllCustmerDtoHandler : IRequestHandler<GetAllCustmersQuery, Result<List<CustomerDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCustmerDtoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<CustomerDto>>> Handle(GetAllCustmersQuery request, CancellationToken cancellationToken)
    {
        List<Customer>? customers = await _unitOfWork.Repository<Customer>().Entities
             .Where(x => x.IsDeleted == false).Include(x => x.Area).ToListAsync();
        List<CustomerDto>? result = [.. customers.ConvertAll(x => (CustomerDto)x)];
        return result != null ? await Result<List<CustomerDto>>.SuccessAsync(result)
        : await Result<List<CustomerDto>>.FailureAsync($"فشل في استرجاع العملاء");
    }
}
