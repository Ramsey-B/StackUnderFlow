using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StackUnderFlow.Business;
using StackUnderFlow.Entities;

namespace StackUnderFlow.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly QuestionsService _questionsService;

        public QuestionsController(QuestionsService questionsService, UserManager<IdentityUser> userManager)
        {
            _questionsService = questionsService;
            _userManager = userManager;
        }

        // GET: Questions
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_questionsService.GetQuestions());
        }

        // GET: Questions/Details/5
        [HttpGet()]
        public IActionResult Details(int id)
        {
            try
            {
                var question = _questionsService.GetQuestionById(id);
                return View(question);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title,Body,Author,AuthorId,Answered,Inappropriate,UpVotes,DownVotes,Id,Topics")]Question newQuestion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception();
                }
                try
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    var question = _questionsService.CreateQuestion(newQuestion, user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            catch
            {
                return View(newQuestion);
            }
        }

        public IActionResult CreateTopic()
        {
            return View();
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int questionId, [Bind("Title,Body,Answered,Inappropriate,UpVotes,DownVotes,Topics")]Question editQuestion)
        {
            try
            {
                editQuestion.Id = questionId;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var newQuestion = _questionsService.EditQuestion(editQuestion, user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(editQuestion);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditQuestionVotes([FromBody]IQuestion editQuestion)
        {
            try
            {
                var newQuestion = _questionsService.EditQuestion(editQuestion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Questions/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Questions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var user = await _userManager.GetUserAsync(HttpContext.User);
                _questionsService.DeleteQuestion(id, user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}