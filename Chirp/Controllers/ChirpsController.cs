using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chirp;
using Chirp.Model;
using Chirp.Services.Services;
using Chirp.Services.Services.Interfaces;
using Chirp.Services.Services.Model.ViewModels;

namespace Chirp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChirpsController : ControllerBase
    {
        private readonly IChirpsService _chirpsService;
        private readonly ILogger<ChirpsController> _logger;

        public ChirpsController(IChirpsService chirpsService, ILogger<ChirpsController> logger)
        {
            _chirpsService = chirpsService;
            _logger = logger;
        }

        // GET: api/Chirps
        [HttpGet]
        public async Task<IActionResult> GetChirpsByFilter([FromQuery] filter filter)
        {
            _logger.LogInformation("Received request to get chirps with filter: {@Filter}", filter);

            List<ChirpViewModel> result = await _chirpsService.GetChirpsByFilter(filter);

            if (result == null || !result.Any())
            {
                _logger.LogInformation("No chirps found for the given filter: {@Filter}", filter);
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Found {Count} chirps for the given filter: {@Filter}", result.Count, filter);
                return Ok(result);
            }
        }

        //// GET: api/Chirps
        //[HttpGet("all")]
        //public async Task<ActionResult<IEnumerable<Chirps>>> GetAllChirps()
        //{
        //    _logger.LogInformation("ChirpsController.GetAllChirps called");

        //    var chirps = await _chirpsService.GetAllChirps();

        //    if (chirps == null || !chirps.Any())
        //    {
        //        _logger.LogInformation("No chirps found in database");
        //        return NoContent();
        //    }

        //    _logger.LogInformation("Returning {Count} chirps", chirps.Count);
        //    return Ok(chirps);
        //    return await _context.Chirps.ToListAsync();
        //}

        //// GET: api/Chirps/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Chirps>> GetChirps([FromRoute] int id)
        //{
        //    var chirps = await _context.Chirps.FindAsync(id);

        //    if (chirps == null)
        //    {
        //        return NotFound();
        //    }

        //    return chirps;
        //}

        //// PUT: api/Chirps/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutChirps([FromRoute] int id, [FromBody] Chirps chirps)
        //{
        //    if (id != chirps.ChirpsId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(chirps).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ChirpsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        //// POST: api/Chirps
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Chirps>> PostChirps([FromBody] Chirps chirps)
        //{
        //    _context.Chirps.Add(chirps);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetChirps", new { id = chirps.ChirpsId }, chirps);
        //}

        //// DELETE: api/Chirps/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteChirps([FromRoute] int id)
        //{
        //    var chirps = await _context.Chirps.FindAsync(id);
        //    if (chirps == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Chirps.Remove(chirps);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ChirpsExists(int id)
        //{
        //    return _context.Chirps.Any(e => e.ChirpsId == id);
        //}
    }
}
