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

        [HttpGet("question/{questionId}")]
        public IActionResult GetResponses(int questionId)
        {
            try
            {
                var responses = _responsesService.GetResponses(questionId);
                return View(responses);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{responseId}")]
        public IActionResult GetResponseById(int responseId)
        {
            try
            {
                var response = _responsesService.GetResponsesById(responseId);
                return View(response);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateResponse([Bind("Body")]Response newResponse)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var response = _responsesService.CreateResponse(newResponse, user);
                return RedirectToAction(nameof(GetResponses));
            }
            catch (Exception)
            {
                return View(newResponse);
            }
        }

        [Authorize]
        [HttpPut("{responseId}")]
        public async Task<IActionResult> EditResponse([Bind("Body")]Response editResponse, int responseId)
        {
            try
            {
                editResponse.Id = responseId;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var newResponse = _responsesService.EditResponse(editResponse, user);
                return RedirectToAction(nameof(GetResponses));
            }
            catch (Exception)
            {
                return View(editResponse);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditResponseVotes([Bind("UpVotes,DownVotes,Inappropriate,Solution")]Response editResponse)
        {
            try
            {
                var newResponse = _responsesService.EditResponse(editResponse);
                return RedirectToAction(nameof(GetResponses));
            }
            catch (Exception)
            {
                return View(editResponse);
            }
        }

        [Authorize]
        [HttpDelete("{responseId}")]
        public async Task<IActionResult> DeleteResponse(int responseId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            _responsesService.DeleteResponse(responseId, user);
            return RedirectToAction(nameof(GetResponses));
        }
    }
}