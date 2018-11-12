using Microsoft.AspNetCore.Identity;
using StackUnderFlow.Business.Validators;
using StackUnderFlow.Data;
using StackUnderFlow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackUnderFlow.Business
{
    public class ResponsesService
    {
        private readonly DataContext _context;

        public ResponsesService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Response> GetResponses(int questionId)
        {
            return _context.Responses.Where(response => response.QuestionId == questionId).ToList()
                .OrderBy(response => response.Solution)
                .ThenBy(response => response.Inappropriate)
                .ThenBy(response => response.UpVotes);
        }

        public Response GetResponsesById(int responseId)
        {
            return _context.Responses.SingleOrDefault(response => response.Id == responseId);
        }

        public Response CreateResponse(Response newResponse, IdentityUser user)
        {
            try
            {
                newResponse.Author = user.UserName;
                newResponse.AuthorId = user.Id;
                newResponse.CreatedDate = DateTime.Now;
                newResponse.UpVotes = 0;
                newResponse.DownVotes = 0;
                newResponse.Inappropriate = 0;
                newResponse.Question = _context.Questions.SingleOrDefault(question => question.Id == newResponse.QuestionId);
                newResponse.Solution = false;
                _context.Responses.Add(newResponse);
                _context.SaveChanges();
                return newResponse;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Response EditResponse(Response editResponse, IdentityUser user)
        {
            try
            {
                var response = _context.Responses.SingleOrDefault(res => res.Id == editResponse.Id && res.AuthorId == user.Id);
                response.Body = editResponse.Body;
                _context.Responses.Update(response);
                _context.SaveChanges();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Response EditResponseVotes(string command, int responseId)
        {
            try
            {
                var response = _context.Responses.SingleOrDefault(res => res.Id == responseId);
                response = ResponseValidator.ValidateCommand(response, command);
                _context.Responses.Update(response);
                _context.SaveChanges();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Response DeleteResponse(int responseId, IdentityUser user)
        {
            try
            {
                var responseToDelete = _context.Responses.SingleOrDefault(response => response.Id == responseId);
                if (responseToDelete.AuthorId == user.Id)
                {
                    _context.Responses.Remove(responseToDelete);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Not Authorized to remove this record");
                }
                return responseToDelete;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Response MarkAsSolution(int responseId, IdentityUser user)
        {
            var response = GetResponsesById(responseId);
            if (user.Id == response.Question.AuthorId)
            {
                response.Solution = !response.Solution;
                _context.Responses.Update(response);
                if (response.Solution)
                {
                    var question = _context.Questions.SingleOrDefault(ques => ques.Id == response.QuestionId);
                    question.Answered = true;
                    _context.Questions.Update(question);
                }
                _context.SaveChanges();
            }
            return response;
        }
    }
}
