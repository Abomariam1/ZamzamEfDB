using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Queries
{
    public record GetAllAreasQuery : IRequest<Result<List<GetAllAreaDto>>>;
    internal class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, Result<List<GetAllAreaDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAreasQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllAreaDto>>> Handle(GetAllAreasQuery query, CancellationToken cancellationToken)
        {
            var areas = await _unitOfWork.Repository<Area>().Entities
                .ProjectTo<GetAllAreaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return await Result<List<GetAllAreaDto>>.SuccessAsync(areas);
        }
    }
}
