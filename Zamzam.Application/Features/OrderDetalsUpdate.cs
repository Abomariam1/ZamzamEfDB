using Microsoft.EntityFrameworkCore;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Types;

namespace Zamzam.Application.Features
{
    public class OrderDetalsUpdate
    {
        private readonly List<OrderDetail> _requestedOrderDetail;
        private readonly IUnitOfWork _unitOfWork;
        public OrderDetalsUpdate(List<OrderDetail> requestedOrderDetail, IUnitOfWork unitOfWork)
        {
            _requestedOrderDetail = requestedOrderDetail;
            _unitOfWork = unitOfWork;
        }
        public async Task GetUpdatedOrderDetails()
        {
            // Get Database Requested OrderDetails
            List<OrderDetail>? dbOrderDetails = _unitOfWork.Repository<OrderDetail>().Entities.Include("Order")
                .Where(x => x.OrderId == _requestedOrderDetail.First().OrderId).ToList();
            // OrderDetail Update Logic
            List<OrderDetail>? orderDetailsToUpdate = new();
            List<OrderDetail>? orderDetailsToAdd = new();
            List<OrderDetail>? orderDetailsToRemove = new();
            // Get the product balance dictionary.
            var itemBalance = new Dictionary<int, int>();
            // Update the product balance for any products that have been added or removed.
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
                    Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(requestedOrderDetail.ItemId);
                    itemBalance[requestedOrderDetail.ItemId] = item.Balance - requestedOrderDetail.Quantity;
                    orderDetailsToAdd.Add(requestedOrderDetail);
                }
                else if (requestedOrderDetail.Quantity != databaseOrderDetail.Quantity
                    || requestedOrderDetail.Price != databaseOrderDetail.Price
                    || requestedOrderDetail.Discount != databaseOrderDetail.Discount)
                {
                    Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(requestedOrderDetail.ItemId);
                    itemBalance[requestedOrderDetail.ItemId] = item.Balance - (requestedOrderDetail.Quantity - databaseOrderDetail.Quantity);
                    orderDetailsToUpdate.Add(requestedOrderDetail);
                }
            }

            foreach (OrderDetail databaseOrderDetail in dbOrderDetails)
            {
                if (_requestedOrderDetail.FirstOrDefault(x => x.ItemId == databaseOrderDetail.ItemId) == null)
                {
                    orderDetailsToRemove.Add(databaseOrderDetail);
                    Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(databaseOrderDetail.ItemId);
                    itemBalance[databaseOrderDetail.ItemId] = item.Balance + databaseOrderDetail.Quantity;
                }
            }

            // Update the database order detail entities.
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
                ReturnSaleOrder returnOrderEntity = new()
                {
                    EmployeeId = orderDetailsToRemove.FirstOrDefault()!.Order.EmployeeId,
                    CreatedBy = orderDetailsToRemove.FirstOrDefault()!.UpdatedBy,
                    OrderType = OrderType.SellReturns,
                    OrderDate = DateTime.Now.ToLocalTime(),
                    InvoiceType = orderDetailsToRemove.FirstOrDefault()!.Order.InvoiceType
                };
                foreach (OrderDetail orderDetail in orderDetailsToRemove)
                {
                    await _unitOfWork.Repository<OrderDetail>().DeleteAsync(orderDetail.Id);
                    returnOrderEntity.TotalPrice += orderDetail.TotalPrice;
                    returnOrderEntity.TotalDiscount += orderDetail.Discount;
                    returnOrderEntity.OrderDetails!.Add(orderDetail);
                }
                await _unitOfWork.Repository<ReturnSaleOrder>().AddAsync(returnOrderEntity);
            }

            foreach (KeyValuePair<int, int> itm in itemBalance)
            {
                Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(itm.Key);
                item.Balance = itm.Value;
            }
        }
    }
}
