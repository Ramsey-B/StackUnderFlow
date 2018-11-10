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
    public class CommentsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CommentsService _commentsService;

        public CommentsController(CommentsService commentsService, UserManager<IdentityUser> userManager)
        {
            _commentsService = commentsService;
            _userManager = userManager;
        }
        
        [HttpGet("response/{responseId}")]
        public ActionResult GetComments(int responseId)
        {
            try
            {
                var comments = _commentsService.GetComments(responseId);
                return View(comments);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
        [HttpGet("{commentId}")]
        public ActionResult GetCommentById(int commentId)
        {
            try
            {
                var comment = _commentsService.GetCommentById(commentId);
                return View(comment);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateComment([Bind("Body,ResponseId")]Comment newComment)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var comment = _commentsService.CreateComment(newComment, user);
                return RedirectToAction(nameof(GetComments));
            }
            catch (Exception)
            {
                return View(newComment);
            }
        }

        [HttpPut("{commentId}")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> EditComment(int commentId, [Bind("Body")]Comment editComment)
        {
            try
            {
                editComment.Id = commentId;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var newResponse = _commentsService.EditComment(editComment, user);
                return RedirectToAction(nameof(GetComments));
            }
            catch (Exception)
            {
                return View(editComment);
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditCommentVotes([Bind("UpVotes,DownVotes,Inappropriate")]Comment editComment)
        {
            try
            {
                var newComment = _commentsService.EditComment(editComment);
                return RedirectToAction(nameof(GetComments));
            }
            catch (Exception)
            {
                return View(editComment);
            }
        }
    }
}