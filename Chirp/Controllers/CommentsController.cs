using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chirp;
using Chirp.Model;
using Chirp.Services.Services.Interfaces;
using Chirp.Services.Services.Model.DTO;

namespace Chirp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentsController> _logger;
        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger)
        {
            _commentService = commentService;
            _logger = logger;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            _logger.LogInformation("CommentsController.GetAllComments called");

            var result = await _commentService.GetComments();

            if (result == null || !result.Any())
            {
                _logger.LogInformation("No comments found.");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Found {Count} comments.", result.Count);
                return Ok(result);
            }
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            _logger.LogInformation("CommentsController.GetCommentsById called");
            var comment = await _commentService.GetCommentById(id);

            if (comment == null)
            {
                _logger.LogInformation("Comment with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Found comment with ID {Id}.", id);
            return Ok(comment);
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment_DTO_Update comment)
        {
            _logger.LogInformation("CommentsController.UpdateComment called");
            var result = await _commentService.UpdateComments(id, comment);
            if (!result)
            {
                _logger.LogWarning("Comment with ID {Id} not found or update failed.", id);
                return NotFound();
            }
            _logger.LogInformation("Comment with ID {Id} updated successfully.", id);
            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment_DTO comment)
        {
            var result = await _commentService.PostComments(comment);
           
            if (result == null)
            {
                _logger.LogWarning("Failed to create comment.");
                return BadRequest("Failed to create comment.");
            }
            _logger.LogInformation("Comment created with ID {Id}.", result);
            return CreatedAtAction("GetComment", new { id = result }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
           var result = await _commentService.DeleteComments(id);
            if (result == null)
            {
                _logger.LogWarning("Comment with ID {Id} not found or delete failed.", id);
                return NotFound();
            }
            _logger.LogInformation("Comment with ID {Id} deleted successfully.", id);
            return NoContent();
        }
    }
}
