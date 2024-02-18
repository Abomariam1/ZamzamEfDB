using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Suppliers.Queries;

public record GetSupplierByIdQuery(int Id) : IRequest<Result<SupplierDto>>;

internal class GetSupplierByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSupplierByIdQuery, Result<SupplierDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<SupplierDto>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        Supplier? supplier = await _unitOfWork.Repository<Supplier>().Entities
            .Where(x => x.Id == request.Id).FirstOrDefaultAsync();
        return supplier != null ? await Result<SupplierDto>.SuccessAsync((SupplierDto)supplier)
            : await Result<SupplierDto>.FailureAsync("Error");

    }
}

