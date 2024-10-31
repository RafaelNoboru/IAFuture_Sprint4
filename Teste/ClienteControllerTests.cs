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
        var options = new DbContextOptionsBuilder<OracleDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        _context = new OracleDbContext(options);
        _context.Clientes.RemoveRange(_context.Clientes); 
        _context.SaveChanges();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "JwtSettings:SecretKey", "jwt_secret_key" },
                { "JwtSettings:Issuer", "TestIssuer" },
                { "JwtSettings:Audience", "TestAudience" },
                { "JwtSettings:ExpiryInHours", "2" }
            })
            .Build();

        _controller = new ClientesController(_context, configuration);
    }

    [Fact]
    public async Task Register_ValidDto_ReturnsCreatedResult()
    {
        // Arrange
        var clienteDto = new ClienteRegisterDto
        {
            Nome = "Test User",
            Usuario = "uniqueuser",  
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
        Assert.Equal("GetCliente", createdResult.ActionName); 
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsOkWithToken()
    {
        // Arrange
        var cliente = new Cliente
        {
            Nome = "Test User",
            Usuario = "testuser",
            Idade = 25,
            Sexo = "Masculino",
            Profissao = "Desenvolvedor",
            EstadoSaude = "Saudável",
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("senha123")
        };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        var loginDto = new ClienteLoginDto
        {
            Usuario = "testuser",
            Senha = "senha123"
        };

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.NotNull(okResult);

        var response = okResult.Value.GetType().GetProperty("Token")?.GetValue(okResult.Value, null) as string;

        Assert.False(string.IsNullOrWhiteSpace(response));
    }
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
    }


}
