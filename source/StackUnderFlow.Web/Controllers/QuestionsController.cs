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
    public class QuestionsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly QuestionsService _questionsService;

        public QuestionsController(QuestionsService questionsService, UserManager<IdentityUser> userManager)
        {
            _questionsService = questionsService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetQuestions()
        {
            return Ok(_questionsService.GetQuestions());
        }

        [HttpGet("{questionId}")]
        public IActionResult GetQuestionById(int questionId)
        {
            try
            {
                var question = _questionsService.GetQuestionById(questionId);
                return Ok(question);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("topic/{topic}")]
        public IActionResult GetQuestionsByTopic(string topic)
        {
            try
            {
                var questions = _questionsService.GetQuestionsByTopic(topic);
                return Ok(questions);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetQuestionsByUserId(string userId)
        {
            try
            {
                var question = _questionsService.GetQuestionsByUserId(userId);
                return Ok(question);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUsersQuestions()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var questions = _questionsService.GetQuestionsByUserId(user.Id);
                return Ok(questions);
            }
            catch(Exception)
            {
                return NotFound();
            }
        } 

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody]Question newQuestion)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var question = _questionsService.CreateQuestion(newQuestion, user);
                return Created("", question);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("{questionId}")]
        public async Task<IActionResult> EditQuestion([FromBody]Question editQuestion, int questionId)
        {
            try
            {
                editQuestion.Id = questionId;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var newQuestion = _questionsService.EditQuestion(editQuestion, user);
                return Ok(newQuestion);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditQuestionVotes([FromBody]Question editQuestion)
        {
            try
            {
                var newQuestion = _questionsService.EditQuestion(editQuestion);
                return Ok(newQuestion);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                _questionsService.DeleteQuestion(questionId, user);
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}