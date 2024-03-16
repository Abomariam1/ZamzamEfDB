using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Items.Commands.Update
{
    public record ItemUpdateCommand: IRequest<Result<ItemDTO>>, IMapFrom<Item>
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal PurchasingPrice { get; set; }
        public decimal SellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public string? UpdatedBy { get; set; }
    }

    internal class ItemUpdateCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<ItemUpdateCommand, Result<ItemDTO>>
    {
        public async Task<Result<ItemDTO>> Handle(ItemUpdateCommand request, CancellationToken cancellationToken)
        {
            Item item = await unitOfWork.Repository<Item>().GetByIdAsync(request.ItemId);
            if(item == null)
                return Result<ItemDTO>.Failure("لم يتم العثور على الصنف");
            item.Name = request.ItemName;
            item.PurchasingPrice = request.PurchasingPrice;
            item.SellingCashPrice = request.SellingCashPrice;
            item.InstallmentPrice = request.InstallmentPrice;
            item.UpdatedBy = request.UpdatedBy;
            Item? result = await unitOfWork.Repository<Item>().UpdateAsync(item);
            result.AddDomainEvent(new ItemUpdatedEvent(item));
            int count = await unitOfWork.Save(cancellationToken);
            ItemDTO? updated = (ItemDTO)result;
            return count > 0 ? await Result<ItemDTO>.SuccessAsync(updated, $"تم تعديل الصنف {result.Name}  بنجاح") :
                await Result<ItemDTO>.FailureAsync($"فشل في تعديل الصنف {result.Name}.");
        }
    }
}
