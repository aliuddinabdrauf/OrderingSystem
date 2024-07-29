using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Dtos
{
    public class PlaceOrderDto
    {
        [Required]
        public Guid MenuId { get; set; }
        [Required]
        public int Total { get; set; }
        [MaxLength(200)]
        public string? Note { get; set; }
    }
    public record OrderDto(Guid Id, string MenuName, int Total, string? Note, Guid TableId, OrderStatus Status, DateTimeOffset UpdateTime)
    {
        public long Version => UpdateTime.ToUnixTimeSeconds();
    };
    //creating class same with record orderDto, since mapster now throw error when mapping into record that have readonly construtor
    public class OrderDto2
    {
        public Guid Id { get; set; }
        public string MenuName { get; set; }
        public Guid MenuId { get; set; }
        public int Total { get; set; }
        public string? Note { get; set; }
        public Guid TableId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTimeOffset UpdateTime { get; set;}
        public long Version => UpdateTime.ToUnixTimeSeconds();
    }
    public record OrderSummaryDto(Guid Id, string Menu, int Total, string? Note, OrderStatus Status, double Price)
    {
        public double TotalPrice => Price * Total;
    }
    public record TableOrderSummaryDto(Guid TableId, string TableNumber, List<OrderSummaryDto> Items)
    {
        public double TotalPriceAll => Items.Sum(x => x.TotalPrice);
        public double TotalOrderAll => Items.Sum(x => x.Total);
    }
}
