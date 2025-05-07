using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSys.Interface;

namespace RestaurantSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOffer _offer;
        public OffersController(IOffer offer)
        {
            _offer = offer;
        }


        [HttpGet("GetAllOffer")]
        public async Task<IActionResult> GetAllActiveOffer()
        {
            try
            {
                var result = await _offer.GetAllActiveOffer();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500 , ex.Message);
            }
        }
    }
}
