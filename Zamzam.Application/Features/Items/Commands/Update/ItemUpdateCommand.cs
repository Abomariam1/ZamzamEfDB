using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Items.Commands.Update
{
    public record ItemUpdateCommand : IRequest<Result<ItemDTO>>, IMapFrom<Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PurchasingPrice { get; set; }
        public decimal SellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public string? UpdatedBy { get; set; }
    }

    internal class ItemUpdateCommandHandler : IRequestHandler<ItemUpdateCommand, Result<ItemDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ItemDTO>> Handle(ItemUpdateCommand request, CancellationToken cancellationToken)
        {
            Item item = await _unitOfWork.Repository<Item>().GetByIdAsync(request.Id);
            if (item == null)
                return Result<ItemDTO>.Failure("لم يتم العثور على الصنف");
            item.Name = request.Name;
            item.PurchasingPrice = request.PurchasingPrice;
            item.SellingCashPrice = request.SellingCashPrice;
            item.InstallmentPrice = request.InstallmentPrice;
            item.UpdatedBy = request.UpdatedBy;
            Item? result = await _unitOfWork.Repository<Item>().UpdateAsync(item);
            result.AddDomainEvent(new ItemUpdatedEvent(item));
            int count = await _unitOfWork.Save(cancellationToken);
            ItemDTO? updated = (ItemDTO)result;
            return count > 0 ? await Result<ItemDTO>.SuccessAsync(updated, $"تم تعديل الصنف {result.Name}  بنجاح") :
                await Result<ItemDTO>.FailureAsync($"فشل في تعديل الصنف {result.Name}.");
        }
    }
}
