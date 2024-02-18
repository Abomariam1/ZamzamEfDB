using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Suppliers.Commands.Delete;

public record SupplierDeleteCommand : IRequest<Result<int>>
{
    public int SupplierId { get; set; }
    public SupplierDeleteCommand()
    {

    }
    public SupplierDeleteCommand(int id)
    {
        SupplierId = id;
    }
}
internal class SupplierDeleteCommandHandler : IRequestHandler<SupplierDeleteCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public SupplierDeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(SupplierDeleteCommand request, CancellationToken cancellationToken)
    {
        Supplier? toDelete = await _unitOfWork.Repository<Supplier>().Entities
            .Where(x => x.Id == request.SupplierId).FirstOrDefaultAsync();
        if (toDelete == null)
        {
            return await Result<int>.FailureAsync($"فشل في حذف المورد");
        }
        Supplier? deleted = await _unitOfWork.Repository<Supplier>().DeleteAsync(request.SupplierId);
        int count = await _unitOfWork.Save(cancellationToken);
        return count > 0 ? await Result<int>.SuccessAsync(deleted.Id, "تم حذف المورد")
            : await Result<int>.FailureAsync("فشل في حذف المورد");
    }
}
