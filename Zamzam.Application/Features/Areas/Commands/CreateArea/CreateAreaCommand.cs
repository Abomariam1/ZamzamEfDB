using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Commands.CreateArea
{
    public record CreateAreaCommand : IRequest<Result<AreaDto>>, IMapFrom<Area>
    {
        public string AreaName { get; set; } = string.Empty;
        public string Station { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public int CollectorId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
    internal class CreateAreaCommandHandler : IRequestHandler<CreateAreaCommand, Result<AreaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AreaDto>> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
        {
            Area? area = new()
            {
                Name = request.AreaName,
                Station = request.Station,
                Location = request.Location,
                EmployeeId = request.CollectorId,
                CreatedBy = request.CreatedBy,
                CreatedDate = request.CreatedDate,
            };
            Area? added = await _unitOfWork.Repository<Area>().AddAsync(area);
            area.AddDomainEvent(new CreatedAreaEvent(added));
            await _unitOfWork.Save(cancellationToken);
            added = await _unitOfWork.Repository<Area>().GetByIdAsync(added.Id);
            AreaDto? conv = (AreaDto)added;
            return await Result<AreaDto>.SuccessAsync(conv, $"تم اضافة {added.Name}");
        }
    }

}
