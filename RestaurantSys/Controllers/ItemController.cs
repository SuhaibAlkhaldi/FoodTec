using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItem _item;
        public ItemController(IItem item)
        {
            _item = item;
        }

        [HttpGet("GetTopRatedItem")]
        public async Task<IActionResult> GetTopRatedItem()
        {
            try
            {
                var result = await _item.GetTopRatedItem();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpGet("GetItemByCategoryID")]
        public async Task<IActionResult> GetItemByCategoryID(int categoryID)
        {
            try
            {
                var result = await _item.GetItemByCategoryID(categoryID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetFavoriteItemByUserID")]
        public async Task<IActionResult> GetFavoriteItem(int userID)
        {
            try
            {
                var result = await _item.GetFavoriteItem(userID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("GetItemById/{id}")]
        public async Task<IActionResult> GetItemById([FromRoute]int id)
        {
            try
            {
                var result = await _item.GetItemById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
