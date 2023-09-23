using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Items.Commands.Update
{
    public record ItemUpdateCommand : IRequest<Result<int>>, IMapFrom<Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PurchasingPrice { get; set; }
        public decimal SellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class ItemUpdateCommandHandler : IRequestHandler<ItemUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(ItemUpdateCommand request, CancellationToken cancellationToken)
        {
            Item item = await _unitOfWork.Repository<Item>().GetByIdAsync(request.Id);
            if (item == null)
                return Result<int>.Failure(0, "لم يتم العثور على الصنف");
            item.Name = request.Name;
            item.PurchasingPrice = request.PurchasingPrice;
            item.SellingCashPrice = request.SellingCashPrice;
            item.InstallmentPrice = request.InstallmentPrice;
            item.UpdatedBy = request.UpdatedBy;
            var result = await _unitOfWork.Repository<Item>().UpdateAsync(item);
            result.AddDomainEvent(new ItemUpdatedEvent(item));
            int count = await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success(count, $"تم تعديل الصنف {result.Name}  بنجاح");
        }
    }
}
