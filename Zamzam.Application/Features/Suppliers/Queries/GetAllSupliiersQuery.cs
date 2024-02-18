using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Suppliers.Queries;

public record GetAllSupliiersQuery : IRequest<Result<List<SupplierDto>>>;

internal class GetAllSupliiersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllSupliiersQuery, Result<List<SupplierDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<SupplierDto>>> Handle(GetAllSupliiersQuery request, CancellationToken cancellationToken)
    {
        List<Supplier>? list = await _unitOfWork.Repository<Supplier>().Entities
            .ToListAsync();
        List<SupplierDto>? result = list.ConvertAll(c => (SupplierDto)c);
        return result != null ? await Result<List<SupplierDto>>.SuccessAsync(result)
            : await Result<List<SupplierDto>>.FailureAsync("Error");
    }
}
