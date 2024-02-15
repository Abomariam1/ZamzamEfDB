using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Items.Commands.Create
{
    public record ItemCreateCommand : IRequest<Result<ItemDTO>>, IMapFrom<Item>
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal PurchasingPrice { get; set; }
        public decimal SellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public int Balance { get; set; }
        public string? CreatedBy { get; set; }
    }

    internal class ItemCreateCommandHandler : IRequestHandler<ItemCreateCommand, Result<ItemDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ItemDTO>> Handle(ItemCreateCommand request, CancellationToken cancellationToken)
        {
            Item item = new()
            {
                Name = request.ItemName,
                PurchasingPrice = request.PurchasingPrice,
                SellingCashPrice = request.SellingCashPrice,
                InstallmentPrice = request.InstallmentPrice,
                Balance = request.Balance,
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Now,
            };
            var result = await _unitOfWork.Repository<Item>().AddAsync(item);
            result.AddDomainEvent(new ItemCreatedEvet(item));
            var count = await _unitOfWork.Save(cancellationToken);
            var resultDto = (ItemDTO)result;
            return count <= 0
                ? await Result<ItemDTO>.FailureAsync(resultDto, $"فشل في اضافة الصنف : {result.Name}")
                : await Result<ItemDTO>.SuccessAsync(resultDto, $"تم اضافة الصنف : {result.Name}");
        }
    }
}
