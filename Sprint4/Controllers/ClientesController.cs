using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint4.Database;
using Sprint4.Models;
using Sprint4.Dtos;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Sprint4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Cadastro de Clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _configuration;

        public ClientesController(OracleDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Clientes/Register
        [HttpPost("Register")]
        public async Task<ActionResult<Cliente>> Register(ClienteRegisterDto clienteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_context.Clientes.Count(c => c.Usuario == clienteDto.Usuario) > 0)
                return BadRequest("Já existe um cliente com esse username.");

            string senhaHash = BCrypt.Net.BCrypt.HashPassword(clienteDto.Senha);

            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                Usuario = clienteDto.Usuario,
                Idade = clienteDto.Idade,
                Sexo = clienteDto.Sexo,
                Profissao = clienteDto.Profissao,
                EstadoSaude = clienteDto.EstadoSaude,
                SenhaHash = senhaHash
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        // POST: api/Clientes/Login
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(ClienteLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Usuario == loginDto.Usuario);
            if (cliente == null)
                return Unauthorized("Cliente não encontrado.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Senha, cliente.SenhaHash);
            if (!isPasswordValid)
                return Unauthorized("Senha inválida.");

            // Gera o token JWT
            var token = GenerateJwtToken(cliente);

            return Ok(new { Token = token });
        }

        // GET: api/Clientes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCliente(int id, ClienteUpdateDto clienteDto)
        {
            if (id != clienteDto.Id)
            {
                return BadRequest();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            if (_context.Clientes.Any(c => c.Usuario == clienteDto.Usuario && c.Id != id))
                return BadRequest("O usuário já está em uso por outro cliente.");

            cliente.Nome = clienteDto.Nome;
            cliente.Usuario = clienteDto.Usuario;
            cliente.Idade = clienteDto.Idade;
            cliente.Sexo = clienteDto.Sexo;
            cliente.Profissao = clienteDto.Profissao;
            cliente.EstadoSaude = clienteDto.EstadoSaude;

            if (!string.IsNullOrEmpty(clienteDto.Senha))
            {
                cliente.SenhaHash = BCrypt.Net.BCrypt.HashPassword(clienteDto.Senha);
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }

        private string GenerateJwtToken(Cliente cliente)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiryInHours = jwtSettings.GetValue<int>("ExpiryInHours");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, cliente.Usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", cliente.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(expiryInHours),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
