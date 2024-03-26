using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Queries.GetAllAreas
{
    public record GetAllAreasQuery: IRequest<Result<List<AreaDto>>>;

    internal class GetAllAreasQueryHandler(IUnitOfWork unitOfWork): IRequestHandler<GetAllAreasQuery, Result<List<AreaDto>>>
    {
        public async Task<Result<List<AreaDto>>> Handle(GetAllAreasQuery query, CancellationToken cancellationToken)
        {
            List<Area>? areas = await unitOfWork.Repository<Area>().Entities
                .Where(x => x.IsDeleted != true).Include(x => x.Employee)
                .ToListAsync(cancellationToken);
            List<AreaDto>? result = [];
            result = [.. areas.ConvertAll(x => (AreaDto)x)];
            //foreach (var area in areas)
            //{
            //    result.Add((AreaDto)area);
            //}
            return await Result<List<AreaDto>>.SuccessAsync(result);
        }
    }
}
