using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Queries.GetAllAreas
{
    public record GetAllAreasQuery : IRequest<Result<List<AreaDto>>>;

    internal class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, Result<List<AreaDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAreasQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<AreaDto>>> Handle(GetAllAreasQuery query, CancellationToken cancellationToken)
        {
            var areas = await _unitOfWork.Repository<Area>().Entities
                .Where(x => x.IsDeleted != true)
                .ProjectTo<AreaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return await Result<List<AreaDto>>.SuccessAsync(areas);
        }
    }
}
