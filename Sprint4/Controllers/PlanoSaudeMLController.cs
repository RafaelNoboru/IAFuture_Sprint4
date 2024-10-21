using Microsoft.AspNetCore.Mvc;
using Sprint4.Models;

[ApiController]
[Route("api/[controller]")]
public class PlanoSaudeMLController : ControllerBase
{
    private readonly PredictionService _predictionService;

    public PlanoSaudeMLController(PredictionService predictionService)
    {
        _predictionService = predictionService;
    }

    [HttpPost("predict")]
    public IActionResult Predict([FromBody] ClientePlanoData cliente)
    {
        try
        {
            var resultado = _predictionService.Predict(cliente);
            var response = new
            {
                PlanoPrevisto = resultado.PlanoEscolhido,
                Scores = resultado.Score
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Erro] {ex.Message}");
            return StatusCode(500, "Erro durante a previsão.");
        }
    }
}
