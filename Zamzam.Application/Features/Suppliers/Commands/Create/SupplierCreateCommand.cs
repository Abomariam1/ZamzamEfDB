using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Suppliers.Commands.Create;

public record SupplierCreateCommand : IRequest<Result<SupplierDto>>
{
    public string SupplierName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
}
internal class SupplierCreateCommandHandler : IRequestHandler<SupplierCreateCommand, Result<SupplierDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public SupplierCreateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SupplierDto>> Handle(SupplierCreateCommand request, CancellationToken cancellationToken)
    {
        Supplier supplier = new()
        {
            Name = request.SupplierName,
            Phone = request.Phone,
            Address = request.Address,
            CreatedBy = request.CreatedBy,
        };
        Supplier? result = await _unitOfWork.Repository<Supplier>().AddAsync(supplier);
        int count = await _unitOfWork.Save(cancellationToken);
        if (count > 0)
        {
            result.AddDomainEvent(new SupplierCreateEvent(result));
            var dto = (SupplierDto)result;
            return await Result<SupplierDto>.SuccessAsync(dto, $"تم انشاء المورد {dto.SupplierName} بنجاح.");
        }
        return await Result<SupplierDto>.FailureAsync($"فشل في انشاء المورد {request.SupplierName}");
    }
}
