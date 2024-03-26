using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Items.Queries.GetAllItems;

public record GetAllItemsQuery: IRequest<Result<List<ItemDTO>>>;
internal class GetAllItemsQueryHandler(IUnitOfWork unitOfWork): IRequestHandler<GetAllItemsQuery, Result<List<ItemDTO>>>
{
    public async Task<Result<List<ItemDTO>>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await unitOfWork.Repository<Item>().Entities
                .Where(x => x.IsDeleted != true)
                .ToListAsync(cancellationToken);
        List<ItemDTO>? result = new();
        result = items.ConvertAll(x => (ItemDTO)x);
        return await Result<List<ItemDTO>>.SuccessAsync(result);
    }
}
