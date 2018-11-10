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
            return _context.Responses.Where(response => response.QuestionId == questionId).ToList();
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
                newResponse.Question = _context.Questions.SingleOrDefault(question => question.Id == newResponse.Id);
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
                if (response.Solution != editResponse.Solution && response.Question.AuthorId == user.Id)
                {
                    response.Solution = editResponse.Solution;
                }
                _context.Responses.Update(response);
                _context.SaveChanges();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Response EditResponse(Response editResponse)
        {
            try
            {
                var response = _context.Responses.SingleOrDefault(res => res.Id == editResponse.Id);
                response = ResponseValidator.ValidateResponseChanges(response, editResponse);
                _context.Responses.Update(response);
                _context.SaveChanges();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteResponse(int responseId, IdentityUser user)
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
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
