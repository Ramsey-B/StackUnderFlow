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
    public class CommentsService
    {
        private readonly DataContext _context;

        public CommentsService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetComments(int responseId)
        {
            return _context.Comments.Where(comment => comment.ResponseId == responseId).ToList()
                .OrderByDescending(comment => comment.UpVotes)
                .ThenBy(comment => comment.Inappropriate);
        }

        public Comment GetCommentById(int commentId)
        {
            return _context.Comments.SingleOrDefault(comment => comment.Id == commentId);
        }

        public Comment CreateComment(Comment newComment, IdentityUser user)
        {
            try
            {
                newComment.Author = user.UserName;
                newComment.AuthorId = user.Id;
                newComment.CreatedDate = DateTime.Now;
                newComment.UpVotes = 0;
                newComment.DownVotes = 0;
                newComment.Inappropriate = 0;
                newComment.Response = _context.Responses.SingleOrDefault(response => response.Id == newComment.ResponseId);
                _context.Comments.Add(newComment);
                _context.SaveChanges();
                return newComment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Comment EditComment(Comment editComment, IdentityUser user)
        {
            try
            {
                var comment = _context.Comments.SingleOrDefault(com => com.Id == editComment.Id && com.AuthorId == user.Id);
                comment.Body = editComment.Body;
                _context.Comments.Update(comment);
                _context.SaveChanges();
                return comment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Comment DeleteComment(int commentId, IdentityUser user)
        {
            try
            {
                var commentToDelete = _context.Comments.SingleOrDefault(comment => comment.Id == commentId);
                if (commentToDelete.AuthorId == user.Id)
                {
                    _context.Comments.Remove(commentToDelete);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Not Authorized to remove this record");
                }
                return commentToDelete;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Comment EditCommentVotes(string command, int commentId)
        {
            try
            {
                var comment = _context.Comments.SingleOrDefault(com => com.Id == commentId);
                comment = CommentValidator.ValidateCommand(comment, command);
                _context.Comments.Update(comment);
                _context.SaveChanges();
                return comment;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
