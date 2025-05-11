using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSys.DTOs.OrderItem.Request;
using RestaurantSys.Interface;

namespace RestaurantSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItem _orderItem;
        public OrderItemController(IOrderItem orderItem)
        {
            _orderItem = orderItem;
        }

        [HttpPost("AddItemToCart")]
        public async Task<IActionResult> AddItemToCart(AddOrderItemDTO item)
        {
            try
            {
                var result = await _orderItem.AddItemToCart(item);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
