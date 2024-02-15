using AutoMapper;
using MediatR;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Commands.DeleteArea
{
    public record AreaDeleteCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public AreaDeleteCommand()
        {

        }

        public AreaDeleteCommand(int id)
        {
            Id = id;
        }
    }
    internal class DeletedAreaCommandHandler : IRequestHandler<AreaDeleteCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletedAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(AreaDeleteCommand command, CancellationToken cancellationToken)
        {
            Area area = await _unitOfWork.Repository<Area>().GetByIdAsync(command.Id);
            if (area != null)
            {
                Area? ar = await _unitOfWork.Repository<Area>().DeleteAsync(area.Id);
                area.AddDomainEvent(new AreaDeletedEvent(area));
                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(area.Id, $"تم حذف {ar.Name} بنجاح.");
            }
            else
                return Result<int>.Failure("لم يتم العثور على المنطقة");
        }
    }
}
