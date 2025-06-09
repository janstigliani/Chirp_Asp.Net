using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chirp;
using Chirp.Model;

namespace Chirp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChirpsController : ControllerBase
    {
        private readonly ChirpContext _context;

        public ChirpsController(ChirpContext context)
        {
            _context = context;
        }

        // GET: api/Chirps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chirps>>> GetChirps()
        {
            return await _context.Chirps.ToListAsync();
        }

        // GET: api/Chirps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chirps>> GetChirps(int id)
        {
            var chirps = await _context.Chirps.FindAsync(id);

            if (chirps == null)
            {
                return NotFound();
            }

            return chirps;
        }

        // PUT: api/Chirps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChirps(int id, Chirps chirps)
        {
            if (id != chirps.ChirpsId)
            {
                return BadRequest();
            }

            _context.Entry(chirps).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChirpsExists(id))
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

        // POST: api/Chirps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chirps>> PostChirps(Chirps chirps)
        {
            _context.Chirps.Add(chirps);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChirps", new { id = chirps.ChirpsId }, chirps);
        }

        // DELETE: api/Chirps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChirps(int id)
        {
            var chirps = await _context.Chirps.FindAsync(id);
            if (chirps == null)
            {
                return NotFound();
            }

            _context.Chirps.Remove(chirps);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChirpsExists(int id)
        {
            return _context.Chirps.Any(e => e.ChirpsId == id);
        }
    }
}
