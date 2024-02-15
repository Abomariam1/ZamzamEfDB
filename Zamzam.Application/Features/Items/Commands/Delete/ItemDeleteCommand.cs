using AutoMapper;
using MediatR;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Items.Commands.Delete
{
    public record ItemDeleteCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public ItemDeleteCommand()
        {

        }

        public ItemDeleteCommand(int id)
        {
            Id = id;
        }
    }

    internal class ItemDeleteCommandHandler : IRequestHandler<ItemDeleteCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(ItemDeleteCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.Repository<Item>().DeleteAsync(request.Id);
            int count = await _unitOfWork.Save(cancellationToken);
            if (item != null)
                return Result<int>.Success(1, $"تم حذف الصنف{item.Name}");
            return Result<int>.Failure(0, "لم يتم العثور على الصنف");
        }
    }
}
