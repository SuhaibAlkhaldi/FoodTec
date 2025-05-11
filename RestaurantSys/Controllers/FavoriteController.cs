using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavorite _favorite;
        public FavoriteController(IFavorite favorite)
        {
            _favorite = favorite;
        }


        [HttpPost("AddItemToFavorite")]
        public async Task<IActionResult> AddItemToFavorite(int userID  , int itemID)
        {
            try
            {
                var result = await _favorite.AddToFavorite(userID, itemID);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("DeleteItemFromFavorite")]
        public async Task<IActionResult> RemoveFromFavorite(int userID, int itemID)
        {
            try
            {
                var result = await _favorite.RemoveFromFavorite(userID, itemID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
