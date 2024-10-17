using Microsoft.AspNetCore.Mvc;
using Sprint4.ML;
using Sprint4.Models;
using Sprint4.Services;

namespace Sprint4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanoSaudeMLController : ControllerBase
    {
        private readonly PredictionService _predictionService;

        public PlanoSaudeMLController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpPost("treinar-modelo")]
        public IActionResult TreinarModelo()
        {
            ModelTraining.TreinarModelo(_predictionService.CaminhoModelo);
            return Ok("Modelo treinado com sucesso!");
        }

        [HttpPost("prever-plano")]
        public IActionResult PreverPlano([FromBody] ClientePlanoData cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Dados do cliente não podem ser nulos.");
            }

            var resultado = _predictionService.Predict(cliente);
            return Ok(resultado);
        }

    }
}
