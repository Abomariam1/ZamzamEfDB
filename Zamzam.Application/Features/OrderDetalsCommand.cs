using Microsoft.EntityFrameworkCore;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Types;

namespace Zamzam.Application.Features
{
    public class OrderDetalsCommand
    {
        private readonly List<OrderDetail> _requestedOrderDetail;
        private readonly IUnitOfWork _unitOfWork;
        public OrderDetalsCommand(List<OrderDetail> requestedOrderDetail, IUnitOfWork unitOfWork)
        {
            _requestedOrderDetail = requestedOrderDetail;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<OrderDetail>> AddSell()
        {
            foreach (var order in _requestedOrderDetail)
            {
                await DecreaseBalance(order);
            }
            return _requestedOrderDetail;
        }

        public async Task<List<OrderDetail>> AddBuy()
        {
            foreach (var order in _requestedOrderDetail)
            {
                await IncreaseBalance(order);
            }
            return _requestedOrderDetail;
        }


        public async Task UpdateSell()
        {
            // Get Database Requested OrderDetails
            List<OrderDetail>? dbOrderDetails = _unitOfWork.Repository<OrderDetail>().Entities.Include("Order")
                .Where(x => x.OrderId == _requestedOrderDetail.First().OrderId).ToList();
            // OrderDetail UpdateSell Logic
            List<OrderDetail>? orderDetailsToUpdate = new();
            List<OrderDetail>? orderDetailsToAdd = new();
            List<OrderDetail>? orderDetailsToRemove = new();
            // Get the product balance dictionary.
            var itemBalance = new Dictionary<int, int>();
            // UpdateSell the product balance for any products that have been added or removed.
            foreach (OrderDetail databaseOrderDetail in dbOrderDetails!)
            {
                Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(databaseOrderDetail!.ItemId);
                itemBalance[item.Id] = item.Balance;
            }
            // Compare the database order detail entities to the requested order detail entities,
            // and handel Item Balance
            foreach (OrderDetail requestedOrderDetail in _requestedOrderDetail)
            {
                OrderDetail? databaseOrderDetail = dbOrderDetails.ToList().Find(x => x.ItemId == requestedOrderDetail.ItemId);
                if (databaseOrderDetail == null)
                {
                    orderDetailsToAdd.Add(requestedOrderDetail);
                    await DecreaseBalance(requestedOrderDetail);
                }
                else if (requestedOrderDetail.Quantity != databaseOrderDetail.Quantity)
                {
                    orderDetailsToUpdate.Add(requestedOrderDetail);
                    await UpdateBalance(requestedOrderDetail, databaseOrderDetail, OrderType.Sell);
                }
            }

            foreach (OrderDetail databaseOrderDetail in dbOrderDetails)
            {
                if (_requestedOrderDetail.FirstOrDefault(x => x.ItemId == databaseOrderDetail.ItemId) == null)
                {
                    orderDetailsToRemove.Add(databaseOrderDetail);
                    await IncreaseBalance(databaseOrderDetail);
                }
            }

            // UpdateSell the database order detail entities.
            foreach (OrderDetail orderDetail in orderDetailsToUpdate)
            {
                await _unitOfWork.Repository<OrderDetail>().UpdateAsync(orderDetail);
            }

            foreach (OrderDetail orderDetail in orderDetailsToAdd)
            {
                await _unitOfWork.Repository<OrderDetail>().AddAsync(orderDetail);
            }

            if (orderDetailsToRemove != null)
            {

                foreach (OrderDetail orderDetail in orderDetailsToRemove)
                {
                    ReturnSaleOrder returnOrderEntity = new()
                    {
                        EmployeeId = orderDetail.Order.EmployeeId,
                        CreatedBy = orderDetail.UpdatedBy,
                        OrderType = OrderType.SellReturns,
                        OrderDate = DateTime.Now.ToLocalTime(),
                        OrderDetails = (ICollection<OrderDetail>)orderDetail,
                        InvoiceType = orderDetail.Order.InvoiceType
                    };
                    returnOrderEntity.TotalPrice += orderDetail.TotalPrice;
                    returnOrderEntity.TotalDiscount += orderDetail.Discount;
                    returnOrderEntity.OrderDetails!.Add(orderDetail);
                    await _unitOfWork.Repository<ReturnSaleOrder>().AddAsync(returnOrderEntity);
                    await _unitOfWork.Repository<OrderDetail>().DeleteAsync(orderDetail.Id);
                }
            }

            //foreach (KeyValuePair<int, int> itm in itemBalance)
            //{
            //    Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(itm.Key);
            //    item.Balance = itm.Value;
            //}
        }

        public async Task<List<OrderDetail>> Delete()
        {

            foreach (var order in _requestedOrderDetail)
            {
                Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(order.ItemId);
                if (item != null)
                {
                    item.Balance += order.Quantity;
                }
                else
                    throw new ItemBalanceException($"الصنف {item!.Name} غير متوفر");

                await _unitOfWork.Repository<Item>().UpdateAsync(item);
            }
            return _requestedOrderDetail;
        }

        private async Task IncreaseBalance(OrderDetail order)
        {
            Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(order.ItemId);
            if (item != null)
            {
                if (item.Balance < order.Quantity || item.Balance == 0)
                    throw new ItemBalanceException($"لا توجد كمية كافية للصنف {item.Name} في المخزن");
                item.Balance += order.Quantity;
            }
            else
                throw new ItemBalanceException($"الصنف {item!.Name} غير متوفر");

            await _unitOfWork.Repository<Item>().UpdateAsync(item);
        }
        private async Task DecreaseBalance(OrderDetail order)
        {
            Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(order.ItemId);
            if (item != null)
            {
                if (item.Balance < order.Quantity || item.Balance == 0)
                    throw new ItemBalanceException($"لا توجد كمية كافية للصنف {item.Name} في المخزن");
                item.Balance -= order.Quantity;
            }
            else
                throw new ItemBalanceException($"الصنف {item!.Name} غير متوفر");

            await _unitOfWork.Repository<Item>().UpdateAsync(item);
        }

        private async Task UpdateBalance(OrderDetail requsted, OrderDetail database, OrderType orderType)
        {
            Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(requsted.ItemId);
            if (item != null)
            {
                switch (orderType)
                {
                    case OrderType.Purchase:
                    case OrderType.SellReturns:
                        item.Balance += (requsted.Quantity - database.Quantity);
                        break;
                    case OrderType.Sell:
                    case OrderType.PurchasReturns:
                    case OrderType.Maintenance:
                        if (item.Balance < requsted.Quantity || item.Balance == 0)
                            throw new ItemBalanceException($"لا توجد كمية كافية للصنف {item.Name} في المخزن");
                        item.Balance -= (requsted.Quantity - database.Quantity);
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
