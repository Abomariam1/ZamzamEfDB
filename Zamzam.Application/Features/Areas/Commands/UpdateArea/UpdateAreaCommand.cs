using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Commands.UpdateArea
{
    public record UpdateAreaCommand : IRequest<Result<AreaDto>>, IMapFrom<Area>
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string Station { get; set; }
        public string? Location { get; set; }
        public int CollectorId { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    }
    internal class UpdateAreaCommandHandler : IRequestHandler<UpdateAreaCommand, Result<AreaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AreaDto>> Handle(UpdateAreaCommand command, CancellationToken cancellationToken)
        {
            Area area = await _unitOfWork.Repository<Area>().GetByIdAsync(command.AreaId);
            if (area == null)
                return Result<AreaDto>.Failure("Area Not Found.");
            else
            {
                area.Name = command.AreaName;
                area.Location = command.Location;
                area.Station = command.Station;
                area.UpdatedBy = command.UpdatedBy;
                area.EmployeeId = command.CollectorId;
                area.UpdatedDate = command.UpdatedDate;
                await _unitOfWork.Repository<Area>().UpdateAsync(area);
                area.AddDomainEvent(new UpdatedAreaEvent(area));
                await _unitOfWork.Save(cancellationToken);
                area = _unitOfWork.Repository<Area>().Entities.Include(x => x.Employee)
                    .FirstOrDefault(x => x.Id == area.Id && x.IsDeleted == false)!;
                return Result<AreaDto>.Success((AreaDto)area, $"تم تعديل المنطقة {area.Name}");
            }
        }
    }
}
