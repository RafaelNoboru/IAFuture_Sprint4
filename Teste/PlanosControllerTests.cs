using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint4.Controllers;
using Sprint4.Database;
using Sprint4.Models;
using Sprint4.Models.Enum;

public class PlanosControllerTests
{
    private readonly PlanosController _controller;
    private readonly OracleDbContext _context;

    public PlanosControllerTests()
    {
        var options = new DbContextOptionsBuilder<OracleDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
             .Options;

        _context = new OracleDbContext(options);
        _controller = new PlanosController(_context);
    }

    [Fact]
    public async Task GetPlanos_ReturnsListOfPlanos()
    {
        // Arrange
        var planoA = new PlanoSaude
        {
            Nome = "Plano A",
            Preco = 150.0,
            TipoPlano = TipoPlano.Basico,
            Cobertura = "Cobertura total",
            PublicoAlvo = PublicoAlvo.Individuais
        };

        var planoB = new PlanoSaude
        {
            Nome = "Plano B",
            Preco = 200.0,
            TipoPlano = TipoPlano.Basico,
            Cobertura = "Cobertura parcial",
            PublicoAlvo = PublicoAlvo.Individuais
        };

        _context.Planos.Add(planoA);
        _context.Planos.Add(planoB);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetPlanos();

        // Assert
        var okResult = Assert.IsType<ActionResult<IEnumerable<PlanoSaude>>>(result);
        var planos = Assert.IsAssignableFrom<IEnumerable<PlanoSaude>>(okResult.Value);
        Assert.Equal(2, planos.Count()); 
    }

    [Fact]
    public async Task GetPlanoSaude_ExistingId_ReturnsPlano()
    {
        // Arrange
        var plano = new PlanoSaude
        {
            Nome = "Plano A",
            Preco = 150.0,
            TipoPlano = TipoPlano.Basico, 
            Cobertura = "Cobertura total",
            PublicoAlvo = PublicoAlvo.Individuais 
        };
        _context.Planos.Add(plano);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetPlanoSaude(plano.Id);

        // Assert
        var okResult = Assert.IsType<ActionResult<PlanoSaude>>(result);
        var retornoPlano = Assert.IsAssignableFrom<PlanoSaude>(okResult.Value);
        Assert.Equal(plano.Id, retornoPlano.Id);
    }

    [Fact]
    public async Task GetPlanoSaude_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = 999; 

        // Act
        var result = await _controller.GetPlanoSaude(nonExistingId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostPlanoSaude_ValidPlano_ReturnsCreatedResult()
    {
        // Arrange
        var validPlano = new PlanoSaude
        {
            Nome = "Plano A",
            Preco = 150.00,
            TipoPlano = TipoPlano.Basico,
            Cobertura = "Cobertura total",
            PublicoAlvo = PublicoAlvo.Individuais
        };

        // Act
        var result = await _controller.PostPlanoSaude(validPlano);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result); 
        var planoRetornado = Assert.IsType<PlanoSaude>(createdResult.Value); 
        Assert.Equal(validPlano.Nome, planoRetornado.Nome); 
    }

    [Fact]
    public async Task DeletePlanoSaude_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var plano = new PlanoSaude
        {
            Nome = "Plano C",
            Preco = 250.0,
            TipoPlano = TipoPlano.Basico, 
            Cobertura = "Cobertura total",
            PublicoAlvo = PublicoAlvo.Individuais 
        };
        _context.Planos.Add(plano);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.DeletePlanoSaude(plano.Id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Null(await _context.Planos.FindAsync(plano.Id)); 
    }

    [Fact]
    public async Task DeletePlanoSaude_NonExistingId_ReturnsNotFound()
    {
        // Act
        var result = await _controller.DeletePlanoSaude(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}





