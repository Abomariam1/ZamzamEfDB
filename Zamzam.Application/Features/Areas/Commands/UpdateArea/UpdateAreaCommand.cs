using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Commands.UpdateArea
{
    public record UpdateAreaCommand : IRequest<Result<int>>, IMapFrom<Area>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Station { get; set; }
        public string Location { get; set; }
        public int? UpdatedBy { get; set; } = 1;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    }
    internal class UpdateAreaCommandHandler : IRequestHandler<UpdateAreaCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateAreaCommand command, CancellationToken cancellationToken)
        {
            Area area = await _unitOfWork.Repository<Area>().GetByIdAsync(command.Id);
            if (area == null)
                return Result<int>.Failure("Area Not Found.");
            else
            {
                area.Name = command.Name;
                area.Location = command.Location;
                area.Station = command.Station;
                area.UpdatedBy = command.UpdatedBy;
                area.UpdatedDate = command.UpdatedDate;
                await _unitOfWork.Repository<Area>().UpdateAsync(area);
                area.AddDomainEvent(new UpdatedAreaEvent(area));
                await _unitOfWork.Save(cancellationToken);
                return Result<int>.Success($"Area {area.Name} Is updated");
            }
        }
    }
}
