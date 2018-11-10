using StackUnderFlow.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Business.Validators
{
    internal static class CommentValidator
    {
        public static Comment ValidateIsAppropriate(Comment comment)
        {
            if (comment.Inappropriate > 5)
            {
                comment.Body = "Great Response!!!";
            }
            return comment;
        }

        public static Comment ValidateCommentChanges(Comment realCom, Comment submitCom)
        {
            if (realCom.UpVotes != submitCom.UpVotes)
            {
                realCom.UpVotes++;
            }
            if (realCom.DownVotes != submitCom.DownVotes)
            {
                realCom.DownVotes++;
            }
            if (realCom.Inappropriate != submitCom.Inappropriate)
            {
                realCom.Inappropriate++;
                realCom = ValidateIsAppropriate(realCom);
            }
            return realCom;
        }
    }
}
