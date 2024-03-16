﻿using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Purchases.Commands.Create
{
    public record PurchaseCreateCommand: IRequest<Result<PurchaseDto>>
    {
        public required DateTime OrderDate { get; set; }
        public required decimal TotalPrice { get; set; }
        public required decimal TotalDiscount { get; set; }
        public required InvoiceType InvoiceType { get; set; }
        public required int EmployeeId { get; set; }
        public required int SupplierId { get; set; }
        public required List<ODetails> Details { get; set; }
        public string? CreatedBy { get; set; }
    }

    internal record PurchaseCreateCommandHndler: IRequestHandler<PurchaseCreateCommand, Result<PurchaseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseCreateCommandHndler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PurchaseDto>> Handle(PurchaseCreateCommand request, CancellationToken cancellationToken)
        {
            List<OrderDetail> details = new();
            List<Item> items = [];
            foreach(ODetails requstDetails in request.Details)
            {
                Item? product = await _unitOfWork.Repository<Item>().GetByIdAsync(requstDetails.ItemId);
                if(product == null)
                    return await Result<PurchaseDto>.FailureAsync($"لم يتم العثور على صنف");
                OrderDetail? detail = new()
                {
                    ItemId = requstDetails.ItemId,
                    Price = requstDetails.Price,
                    Quantity = requstDetails.Quantity,
                    Discount = requstDetails.Discount,
                    CreatedBy = requstDetails.CreatedBy
                };
                product.Balance += requstDetails.Quantity;
                items.Add(product);
                details.Add(detail);
            }
            PurchaseOrder purchase = new()
            {
                SupplierId = request.SupplierId,
                OrderDate = request.OrderDate,
                TotalPrice = request.TotalPrice,
                TotalDiscount = request.TotalDiscount,
                OrderType = OrderType.Purchase,
                EmployeeId = request.EmployeeId,
                OrderDetails = details,
                CreatedBy = request.CreatedBy,
            };
            purchase.OrderDetails = details;
            PurchaseOrder? order = await _unitOfWork.Repository<PurchaseOrder>().AddAsync(purchase);
            int count = await _unitOfWork.Save(cancellationToken);
            order.AddDomainEvent(new PurchaseCreatedEvent(purchase));
            if(count > 0)
            {
                foreach(var item in items)
                {
                }
                items.ForEach(x => _unitOfWork.Repository<Item>().UpdateAsync(x));
            }
            int itemsAdded = await _unitOfWork.Save(cancellationToken);

            return count > 0 ? await Result<PurchaseDto>.SuccessAsync((PurchaseDto)order, "تم اضافة فاتورة الشراء بنجاح")
                : await Result<PurchaseDto>.FailureAsync("فشل في انشاء فاتورة شراء");
        }
    }
}
