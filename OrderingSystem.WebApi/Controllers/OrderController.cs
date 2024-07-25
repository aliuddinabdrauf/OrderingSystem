using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Application.Services;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using OrderingSystem.Infrastructure.Dtos;

namespace OrderingSystem.WebApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController(IOrderService orderService, UserManager<IdentityUser<Guid>> userManager) : ControllerBase
    {
        [HttpGet]
        [Route("all/active")]
        [ProducesResponseType(typeof(List<OrderDto>), 200)]
        public async Task<IActionResult> GetAllActiveOrders()
        {
            var result = await orderService.GetAllActiveOrders();
            return Ok(result);
        }
        [HttpGet]
        [Route("{tableId}/active")]
        [ProducesResponseType(typeof(List<OrderDto>), 200)]
        public async Task<IActionResult> GetActiveTableOrder(Guid tableId)
        {
            var result = await orderService.GetActiveTableOrder(tableId);
            return Ok(result);
        }
        [HttpPost]
        [Route("create/{tableId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PlaceOrder(Guid tableId, [FromBody]List<PlaceOrderDto> orders)
        {
            await orderService.PlaceOrder(tableId, orders);
            return NoContent();
        }

        [HttpPatch]
        [Route("{orderId}/{orderStatus}")]
        [Authorize]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, OrderStatus orderStatus)
        {
            Guid userId = new Guid(userManager.GetUserId(User));
            await orderService.UpdateOrderStatus(orderId, orderStatus, userId);
            return NoContent();
        }
    }
}
