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
using Microsoft.Extensions.Logging;
using Chirp.Services.Services.Model.Filter;
using Chirp.Services.Services.Model.DTO;

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

        // GET: api/Chirps
        [HttpGet("all")]
        public async Task<IActionResult> GetAllChirps()
        {
            _logger.LogInformation("ChirpsController.GetAllChirps called");

            var chirps = await _chirpsService.GetAllChirps();

            if (chirps == null || !chirps.Any())
            {
                _logger.LogInformation("No chirps found in database");
                return NoContent();
            }

            _logger.LogInformation("Returning {Count} chirps", chirps.Count);
            return Ok(chirps);
        }

        // GET: api/Chirps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChirpsById([FromRoute] int id)
        {
            _logger.LogInformation("ChirpsController.GetChirpsById called");

            var chirps = await _chirpsService.GetChirpsById(id);

            if (chirps == null)
            {
                _logger.LogInformation("No chirps found in database");
                return NoContent();
            }

            _logger.LogInformation("Returning chirp result");
            return Ok(chirps);
        }

        // PUT: api/Chirps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChirps([FromRoute] int id, [FromBody] Chirps_DTO_Update chirps)
        {
            _logger.LogInformation("ChirpsController.UpdateChirps called");
            var result = await _chirpsService.UpdateChirps(id, chirps);
            if (!result)
            {
                _logger.LogWarning("Failed to update chirp with ID {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Successfully updated chirp with ID {Id}", id);
            return NoContent();
        }


        // POST: api/Chirps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostChirps([FromBody] Chirps_DTO chirps)
        {
            _logger.LogInformation("ChirpsController.PostChirps called");
            var result = await _chirpsService.PostChirps(chirps);
            if (result == null)
            {
                _logger.LogWarning("Failed to create chirp");
                return BadRequest("Failed to create chirp");
            }
            _logger.LogInformation("Successfully created chirp with ID {Id}", result);
            return CreatedAtAction("GetChirpsById", new { id = result }, chirps);
        }

        // delete: api/chirps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> deletechirps([FromRoute] int id)
        {
            _logger.LogInformation("ChirpsController.DeleteChirps called");
            var result = await _chirpsService.DeleteChirps(id);
            if (result == null)
            {
                _logger.LogWarning("Failed to delete chirp with ID {Id}", id);
                return NotFound();
            }
            if (result == -1)
            {
                _logger.LogWarning("Warning, first you have to eliminate all associates comments of Chirps with ID:{id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Successfully deleted chirp with ID {Id}", id);
            return NoContent();
        }
    }
}
