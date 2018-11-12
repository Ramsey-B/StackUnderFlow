using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackUnderFlow.Business;
using StackUnderFlow.Entities;

namespace StackUnderFlow.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CommentsService _commentsService;

        public CommentController(CommentsService commentsService, UserManager<IdentityUser> userManager)
        {
            _commentsService = commentsService;
            _userManager = userManager;
        }

        [HttpGet("response/{responseId}")]
        public IActionResult GetComments(int responseId)
        {
            try
            {
                var comments = _commentsService.GetComments(responseId);
                return Ok(comments);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{commentId}")]
        public IActionResult GetCommentById(int commentId)
        {
            try
            {
                var comment = _commentsService.GetCommentById(commentId);
                return Ok(comment);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]Comment newComment)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var comment = _commentsService.CreateComment(newComment, user);
                return Created("", comment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("{commentId}")]
        public async Task<IActionResult> EditQuestion([FromBody]Comment editComment, int commentId)
        {
            try
            {
                editComment.Id = commentId;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var newComment = _commentsService.EditComment(editComment, user);
                return Ok(newComment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("{command}/{commentId}")]
        public IActionResult EditResponseVotes(string command, int commentId)
        {
            try
            {
                var newComment = _commentsService.EditCommentVotes(command, commentId);
                return Ok(newComment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                _commentsService.DeleteComment(commentId, user);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}