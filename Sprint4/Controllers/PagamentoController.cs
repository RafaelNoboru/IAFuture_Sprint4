using Microsoft.AspNetCore.Mvc;
using Sprint4.Services;

namespace Sprint4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly StripeService _stripeService;

        public PagamentoController(StripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("processar")]
        public async Task<IActionResult> ProcessarPagamento([FromBody] decimal valor)
        {
            try
            {
                var paymentIntent = await _stripeService.CreatePaymentIntent(valor);
                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
