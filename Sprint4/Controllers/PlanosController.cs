using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint4.Database;
using Sprint4.Models;

namespace Sprint4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Cadastro de Planos")]
    public class PlanosController : ControllerBase
    {
        private readonly OracleDbContext _context;

        public PlanosController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: api/Planos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanoSaude>>> GetPlanos()
        {
            return await _context.Planos.ToListAsync();
        }

        // GET: api/Planos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanoSaude>> GetPlanoSaude(int id)
        {
            var planoSaude = await _context.Planos.FindAsync(id);

            if (planoSaude == null)
            {
                return NotFound();
            }

            return planoSaude;
        }

        // PUT: api/Planos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanoSaude(int id, PlanoSaude planoSaude)
        {
            if (id != planoSaude.Id)
            {
                return BadRequest();
            }

            _context.Entry(planoSaude).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanoSaudeExists(id))
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

        // POST: api/Planos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlanoSaude>> PostPlanoSaude(PlanoSaude planoSaude)
        {
            _context.Planos.Add(planoSaude);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlanoSaude", new { id = planoSaude.Id }, planoSaude);
        }

        // DELETE: api/Planos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanoSaude(int id)
        {
            var planoSaude = await _context.Planos.FindAsync(id);
            if (planoSaude == null)
            {
                return NotFound();
            }

            _context.Planos.Remove(planoSaude);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanoSaudeExists(int id)
        {
            return _context.Planos.Any(e => e.Id == id);
        }
    }
}
