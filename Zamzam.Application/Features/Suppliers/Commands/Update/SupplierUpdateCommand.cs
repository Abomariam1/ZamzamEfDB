using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Suppliers.Commands.Update;

public record SupplierUpdateCommand : IRequest<Result<SupplierDto>>
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
}
internal class SupplierUpdateCommandHandler : IRequestHandler<SupplierUpdateCommand, Result<SupplierDto>>
{
    private IUnitOfWork _unitOfWork;

    public SupplierUpdateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SupplierDto>> Handle(SupplierUpdateCommand request, CancellationToken cancellationToken)
    {
        Supplier? toUpdate = await _unitOfWork.Repository<Supplier>().Entities
            .Where(x => x.Id == request.SupplierId).FirstOrDefaultAsync();
        if (toUpdate == null)
        {
            return await Result<SupplierDto>.FailureAsync($"فشل في تعديل المورد {request.SupplierName}.");
        }
        toUpdate.Id = request.SupplierId;
        toUpdate.Name = request.SupplierName;
        toUpdate.Phone = request.Phone;
        toUpdate.Address = request.Address;
        toUpdate.UpdatedBy = request.UpdatedBy;
        toUpdate.UpdatedDate = request.UpdatedOn;
        Supplier? updated = await _unitOfWork.Repository<Supplier>().UpdateAsync(toUpdate);
        SupplierDto dto = (SupplierDto)updated;
        int count = await _unitOfWork.Save(cancellationToken);
        updated.AddDomainEvent(new SupplierUpdateEvent(updated));
        return count > 0 ? await Result<SupplierDto>.SuccessAsync(dto, $"تم تعديل العميل {dto.SupplierName} بنجاح")
            : await Result<SupplierDto>.FailureAsync($"فشل في تعديل العميل {dto.SupplierName}");
    }
}
