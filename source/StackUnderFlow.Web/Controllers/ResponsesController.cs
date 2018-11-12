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
    public class ResponsesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ResponsesService _responsesService;

        public ResponsesController(ResponsesService responsesService, UserManager<IdentityUser> userManager)
        {
            _responsesService = responsesService;
            _userManager = userManager;
        }
        
        public IActionResult GetResponses(int id)
        {
            try
            {
                ViewData["QuestionId"] = id;
                var responses = _responsesService.GetResponses(id);
                return View(responses);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
       
        public IActionResult GetResponseById(int id)
        {
            try
            {
                var response = _responsesService.GetResponsesById(id);
                return View(response);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public IActionResult CreateResponse(int id)
        {
            ViewData["QuestionId"] = id;
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateResponse([Bind("Body")]Response newResponse, int id)
        {
            try
            {
                newResponse.QuestionId = id;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var response = _responsesService.CreateResponse(newResponse, user);
                return RedirectToAction(nameof(GetResponses), new { id = response.QuestionId });
            }
            catch (Exception)
            {
                return View(newResponse);
            }
        }

        public IActionResult EditResponse(int id)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditResponse([Bind("Body, Solution")]Response editResponse, int id)
        {
            try
            {
                editResponse.Id = id;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var newResponse = _responsesService.EditResponse(editResponse, user);
                return RedirectToAction(nameof(GetResponses), new { id = newResponse.QuestionId });
            }
            catch (Exception)
            {
                return View(editResponse);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditResponseVotes(string command, int id)
        {
            var newResponse = _responsesService.EditResponseVotes(command, id);
            return RedirectToAction(nameof(GetResponses), new { id = newResponse.QuestionId });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> MarkSolution(int responseId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var response = _responsesService.MarkAsSolution(responseId, user);
            return RedirectToAction(nameof(GetResponses), new { id = response.QuestionId });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteResponse(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var deletedResponse = _responsesService.DeleteResponse(id, user);
            return RedirectToAction(nameof(GetResponses), new { id = deletedResponse.QuestionId });
        }
    }
}