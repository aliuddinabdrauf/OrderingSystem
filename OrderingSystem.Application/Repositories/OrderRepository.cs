using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using OrderingSystem.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderingSystem.Application.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderDto>> GetActiveOrderByTable(Guid tableId);
    }

    public class OrderRepository(OrderingSystemDbContext context) : IOrderRepository
    {
        private IQueryable<OrderDto> GetOrders()
        {
            var result = context.TblOrder.Include(o => o.Menu).Select(o =>
            new OrderDto(o.Id, o.Menu.Name, o.Total, o.Note, o.TableId, o.Status))
                .AsQueryable();
            return result;
        }
        public async Task<List<OrderDto>> GetActiveOrderByTable(Guid tableId)
        {
            var result = await GetOrders().Where(o => o.TableId == tableId && new OrderStatus[] { OrderStatus.Completed, OrderStatus.Preparing, OrderStatus.Placed }.Contains(o.Status))
                .ToListAsync();
            return result;
        }
    }
}
