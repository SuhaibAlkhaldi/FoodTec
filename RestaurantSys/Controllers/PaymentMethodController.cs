using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSys.DTOs.Payment.Request;
using RestaurantSys.Interface;

namespace RestaurantSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethod _paymentMethd;
        public PaymentMethodController(IPaymentMethod paymentMethd)
        {
            _paymentMethd = paymentMethd;
        }


        [HttpPost("AddPaymentMethod")]
        public async Task<IActionResult> AddPaymentMethod(AddPaymentMethodDTO input)
        {
            try
            {
                var result = await _paymentMethd.AddPaymentMethod(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
