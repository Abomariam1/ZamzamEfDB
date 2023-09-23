using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Items.Commands.Create
{
    public record ItemCreateCommand : IRequest<Result<int>>, IMapFrom<Item>
    {
        public string Name { get; set; }
        public decimal PurchasingPrice { get; set; }
        public decimal SellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public int Balance { get; set; }
        public string? CreatedBy { get; set; }
    }

    internal class ItemCreateCommandHandler : IRequestHandler<ItemCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(ItemCreateCommand request, CancellationToken cancellationToken)
        {
            Item item = new()
            {
                Name = request.Name,
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
            return await Result<int>.SuccessAsync(count, $"تم اضافة الصنف : {result.Name}");
        }
    }
}
