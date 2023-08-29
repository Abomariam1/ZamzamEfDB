using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Commands.DeleteArea
{
    public record DeleteAreaCommand : IRequest<Result<int>>, IMapFrom<Area>
    {
        public int Id { get; }
        public DeleteAreaCommand()
        {

        }
        public DeleteAreaCommand(int id)
        {
            Id = id;
        }

    }
    internal class DeletedAreaCommandHandler : IRequestHandler<DeleteAreaCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletedAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DeleteAreaCommand command, CancellationToken cancellationToken)
        {
            Area area = await _unitOfWork.Repository<Area>().GetByIdAsync(command.Id);
            if (area != null)
            {
                await _unitOfWork.Repository<Area>().DeleteAsync(command.Id);
                area.AddDomainEvent(new DeletedAreaEvent(area));
                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(area.Id, "Area Deleted.");
            }
            else
                return Result<int>.Failure("Area Not Found");
        }
    }
}
