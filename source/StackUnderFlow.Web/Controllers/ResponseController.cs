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
    public class ResponseController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ResponsesService _responsesService;

        public ResponseController(ResponsesService responsesService, UserManager<IdentityUser> userManager)
        {
            _responsesService = responsesService;
            _userManager = userManager;
        }

        [HttpGet("question/questionId")]
        public IActionResult GetResponses(int questionId)
        {
            try
            {
                var responses = _responsesService.GetResponses(questionId);
                return Ok(responses);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("responseId")]
        public IActionResult GetResponseById(int responseId)
        {
            try
            {
                var response = _responsesService.GetResponsesById(responseId);
                return Ok(response);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]Response newResponse)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var response = _responsesService.CreateResponse(newResponse, user);
                return Created("", response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("{responseId}")]
        public async Task<IActionResult> EditResponse([FromBody]Response editResponse, int responseId)
        {
            try
            {
                editResponse.Id = responseId;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var newResponse = _responsesService.EditResponse(editResponse, user);
                return Ok(newResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditResponseVotes([FromBody]Response editResponse)
        {
            try
            {
                var newResponse = _responsesService.EditResponse(editResponse);
                return Ok(newResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete("{responseId}")]
        public async Task<IActionResult> DeleteResponse(int responseId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                _responsesService.DeleteResponse(responseId, user);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}