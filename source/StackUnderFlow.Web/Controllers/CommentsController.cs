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
        
        public ActionResult GetComments(int id)
        {
            try
            {
                ViewData["ResponseId"] = id;
                var comments = _commentsService.GetComments(id);
                return View(comments);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
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

        public ActionResult CreateComment(int id)
        {
            ViewData["ResponseId"] = id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateComment([Bind("Body,ResponseId")]Comment newComment, int id)
        {
            try
            {
                newComment.ResponseId = id;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var comment = _commentsService.CreateComment(newComment, user);
                return RedirectToAction(nameof(GetComments));
            }
            catch (Exception)
            {
                return View(newComment);
            }
        }

        public IActionResult EditComment(int id)
        {
            return View();
        }

        [HttpPost]
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

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditCommentVotes(string command, int commentId)
        {
            var newComment = _commentsService.EditCommentVotes(command, commentId);
            return RedirectToAction(nameof(GetComments), new { id = newComment.ResponseId });
        }

        [HttpGet]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var deletedComment = _commentsService.DeleteComment(commentId, user);
            return RedirectToAction(nameof(GetComments), new { deletedComment.ResponseId });
        }
    }
}