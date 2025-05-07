using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSys.Interface;

namespace RestaurantSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _notification;
        public NotificationController(INotification notification)
        {
            _notification = notification;
        }


        [HttpGet("GetAllNotificationByUserID")]
        public async Task<IActionResult> GetAllNotification(int userID)
        {
            try
            {
                var result = await _notification.GetAllNotification(userID);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
