using AutoMapper;
using MediatR;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Application.Mappings;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Commands.CreateArea
{
    public record CreateAreaCommand : IRequest<Result<int>>, IMapFrom<Area>
    {
        public required string Name { get; set; } = string.Empty;
        public required string Station { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
    }
    internal class CreateAreaCommandHandler : IRequestHandler<CreateAreaCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
        {
            var area = new Area()
            {
                Name = request.Name,
                Station = request.Station,
                Location = request.Location,
            };
            await _unitOfWork.Repository<Area>().AddAsync(area);
            area.AddDomainEvent(new CreatedAreaEvent(area));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(area.Id, "Area Created.");
        }
    }

}
