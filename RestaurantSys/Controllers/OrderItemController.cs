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




        [HttpGet("GetCurrentCart")]
        public async Task<IActionResult> GetCurrentCart(int UserID)
        {
            try
            {
                var result = await _orderItem.GetCurrentCart(UserID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("AddOneToCart")]
        public async Task<IActionResult> AddOneToCart(AddToCart input)
        {
            try
            {
                var result = await _orderItem.AddOneToCart(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll(int userID, int itemID)
        {
            try
            {
                var result = await _orderItem.RemoveAll(userID, itemID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("DeleteOne")]
        public async Task<IActionResult> DeleteOne(int userID, int itemID)
        {
            try
            {
                var result = await _orderItem.RemoveOne(userID, itemID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
