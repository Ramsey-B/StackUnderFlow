using StackUnderFlow.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Business.Validators
{
    internal static class CommentValidator
    {
        internal static Comment ValidateIsAppropriate(Comment comment)
        {
            if (comment.Inappropriate > 5)
            {
                comment.Body = "Great Response!!!";
            }
            return comment;
        }

        internal static Comment ValidateCommand(Comment comment, string command)
        {
            if (command == "upvote")
            {
                comment.UpVotes++;
            }
            if (command == "downvote")
            {
                comment.DownVotes++;
            }
            if (command == "innappropriate")
            {
                comment.Inappropriate++;
                comment = ValidateIsAppropriate(comment);
            }
            return comment;
        }
    }
}
