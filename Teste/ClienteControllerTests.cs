using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sprint4.Controllers;
using Sprint4.Database;
using Sprint4.Dtos;
using Sprint4.Models;

public class ClientesControllerTests
{
    private readonly OracleDbContext _context;
    private readonly ClientesController _controller;

    public ClientesControllerTests()
    {
        // Configuração do banco de dados em memória
        var options = new DbContextOptionsBuilder<OracleDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new OracleDbContext(options);
        _controller = new ClientesController(_context, new ConfigurationBuilder().Build());
    }

    [Fact]
    public async Task Register_ValidDto_ReturnsCreatedResult()
    {
        // Arrange
        var clienteDto = new ClienteRegisterDto
        {
            Nome = "Test User",
            Usuario = "rafael", 
            Idade = 30,
            Sexo = "Masculino",
            Profissao = "Desenvolvedor",
            EstadoSaude = "Saudável",
            Senha = "senha123"
        };

        // Act
        var result = await _controller.Register(clienteDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.NotNull(createdResult);
        Assert.Equal("GetCliente", createdResult.ActionName); // Verifica a ação esperada
    }



    [Fact]
    public async Task Register_DuplicateUsername_ReturnsBadRequest()
    {
        // Arrange
        var clienteDto = new ClienteRegisterDto
        {
            Nome = "Test User",
            Usuario = "testuser",
            Senha = "senha123"
        };

        var existingCliente = new Cliente
        {
            Nome = "Existing User",
            Usuario = "testuser",
            Idade = 25,
            Sexo = "Feminino",
            Profissao = "Engenheira",
            EstadoSaude = "Saudável",
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("senha123")
        };
        _context.Clientes.Add(existingCliente);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.Register(clienteDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}
