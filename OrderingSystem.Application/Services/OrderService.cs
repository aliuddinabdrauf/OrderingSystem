using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OrderingSystem.Application.Hubs;
using OrderingSystem.Application.Repositories;
using OrderingSystem.Infrastructure;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using OrderingSystem.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Application.Services
{
    public interface IOrderService
    {
        Task<bool> AllOrderIsValidToComplete(List<Guid> orderIds);
        Task<List<OrderDto>> GetActiveTableOrder(Guid tableId);
        Task<List<OrderDto>> GetAllActiveOrders();
        Task<TableOrderSummaryDto> GetTableOrderSummary(Guid tableId);
        Task PlaceOrder(Guid tableId, List<PlaceOrderDto> orders);
        Task UpdateOrderStatus(Guid orderId, OrderStatus orderStatus, Guid userId, long version = 0);
    }

    public class OrderService(IBaseRepository baseRepository, IOrderRepository orderRepository, IHubContext<OrderHub> orderHubContext) : IOrderService
    {
        public async Task PlaceOrder(Guid tableId, List<PlaceOrderDto> orders)
        {
            var toSave = orders.Adapt<List<TblOrder>>();
            foreach (var t in toSave)
            {
                t.TableId = tableId;
            }
            baseRepository.AddDataBatch(toSave);
            await baseRepository.SaveChanges();
            //send new order data realtime to the client
            await orderHubContext.Clients.All.SendAsync("new-order", toSave.Adapt<List<OrderDto2>>());
        }
        public async Task<List<OrderDto>> GetAllActiveOrders()
        {
            var result = await orderRepository.GetAllActiveOrders();
            return result;
        }
        public async Task<List<OrderDto>> GetActiveTableOrder(Guid tableId)
        {
            var result = await orderRepository.GetActiveOrderByTable(tableId);
            return result;
        }
        public async Task UpdateOrderStatus(Guid orderId, OrderStatus orderStatus, Guid userId, long version = 0)
        {
            var toUpdate = await baseRepository.GetDataById<TblOrder>(orderId);
            if (version != 0 && toUpdate.TimestampUpdated.ToUnixTimeSeconds() != version)
                throw new ActionNotValidException("Data had been change since retreiving data");
            toUpdate.Status = orderStatus;
            await baseRepository.SaveChanges(userId);
            var latestVersion = toUpdate.TimestampUpdated.ToUnixTimeSeconds();
            //send updated event realtime
            await orderHubContext.Clients.All.SendAsync("order-updated", new {Id = orderId, Status = orderStatus, Version = latestVersion });
        }
        public async Task<TableOrderSummaryDto> GetTableOrderSummary(Guid tableId)
        {
            var result = await orderRepository.GetTableOrderSummary(tableId);
            return result;
        }
        public async Task<bool> AllOrderIsValidToComplete(List<Guid> orderIds)
        {
            if(orderIds.Count == 0)
                return false;
            foreach (var orderId in orderIds)
            {
                var valid = await baseRepository.IsExistAsync<TblOrder>(o => o.Id == orderId && o.Status != OrderStatus.Paid && o.Status != OrderStatus.Rejected);
                if(!valid)
                    return false;
            }
            return true;
        }
    }
}
