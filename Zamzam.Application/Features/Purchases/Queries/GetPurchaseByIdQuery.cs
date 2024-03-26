using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Purchases.Queries;
public record GetPurchaseByIdQuery(int invId): IRequest<Result<PurchaseDto>>;

internal class GetPurchaseByIdQueryHandler(IUnitOfWork unitOfWork): IRequestHandler<GetPurchaseByIdQuery, Result<PurchaseDto>>
{
    public async Task<Result<PurchaseDto>> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
    {
        PurchaseOrder? result = unitOfWork.Repository<PurchaseOrder>().Entities
            .Where(x => x.IsDeleted == false)
            .Include(x => x.OrderDetails)
            .FirstOrDefault(d => d.Id == request.invId);
        result.OrderDetails.ToList().ForEach(x => x.Item = unitOfWork.Repository<Item>().Entities
        .SingleOrDefault(s => s.Id == x.ItemId));
        if(result == null)
            return await Result<PurchaseDto>.FailureAsync("لم يتم ايجاد الفاتورة");
        PurchaseDto dto = (PurchaseDto)result;
        return await Result<PurchaseDto>.SuccessAsync(dto);
    }
}
