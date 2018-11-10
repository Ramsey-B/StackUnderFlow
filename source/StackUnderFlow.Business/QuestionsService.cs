using Microsoft.AspNetCore.Identity;
using StackUnderFlow.Business.Validators;
using StackUnderFlow.Data;
using StackUnderFlow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackUnderFlow.Business
{
    public class QuestionsService
    {
        private readonly DataContext _context;

        public QuestionsService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Question> GetQuestions()
        {
            return _context.Questions.ToList();
        }

        public Question GetQuestionById(int questionId)
        {
            return _context.Questions.SingleOrDefault(question => question.Id == questionId);
        }

        public IEnumerable<Question> GetQuestionsByTopic(string topicName)
        {
            return _context.QuestionTopics
                .Where(questionTopic => questionTopic.Topic.Name == topicName)
                .Select(questionTopic => questionTopic.Question).ToList();
        }

        public IEnumerable<Question> GetQuestionsByUserId(string userId)
        {
            return _context.Questions.Where(question => question.AuthorId == userId);
        }

        public Question CreateQuestion(Question newQuestion, IdentityUser user)
        {
            try
            {
                newQuestion.Author = user.UserName;
                newQuestion.AuthorId = user.Id;
                newQuestion.Inappropriate = 0;
                newQuestion.UpVotes = 0;
                newQuestion.DownVotes = 0;
                newQuestion.CreatedDate = DateTime.Now;
                _context.Questions.Add(newQuestion);
                _context.SaveChanges();
                return newQuestion;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Question EditQuestion(Question editQuestion, IdentityUser user)
        {
            try
            {
                var question = _context.Questions.SingleOrDefault(ques => ques.Id == editQuestion.Id && ques.AuthorId == user.Id);
                question.Title = editQuestion.Title;
                question.Body = editQuestion.Body;
                question.Answered = editQuestion.Answered;
                _context.Questions.Update(question);
                _context.SaveChanges();
                return question;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Question EditQuestion(IQuestion editQuestion)
        {
            try
            {
                var question = _context.Questions.SingleOrDefault(ques => ques.Id == editQuestion.Id);
                question = QuestionValidator.ValidateQuestionChanges(question, editQuestion);
                _context.Questions.Update(question);
                _context.SaveChanges();
                return question;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void DeleteQuestion(int questionId, IdentityUser user)
        {
            try
            {
                var questionToDelete = _context.Questions.SingleOrDefault(question => question.Id == questionId);
                if (questionToDelete.AuthorId == user.Id)
                {
                    _context.Questions.Remove(questionToDelete);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Not Authorized to remove this record");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
