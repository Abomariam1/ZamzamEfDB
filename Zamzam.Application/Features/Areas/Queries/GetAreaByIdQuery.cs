using AutoMapper;
using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Queries
{
    public record GetAreaByIdQuery : IRequest<Result<AreaDto>>
    {
        public int Id { get; set; }
        public GetAreaByIdQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQuery, Result<AreaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAreaByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AreaDto>> Handle(GetAreaByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Area>().GetByIdAsync(query.Id);
            if (entity == null)
                return Result<AreaDto>.Failure("Area Not Found");
            else
            {
                var area = _mapper.Map<AreaDto>(entity);
                return await Result<AreaDto>.SuccessAsync(area);
            }
        }
    }
}
