using StackUnderFlow.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Business.Validators
{
    internal static class QuestionValidator
    {
        internal static Question ValidateIsAppropriate(Question question)
        {
            if (question.Inappropriate > 5)
            {
                question.Title = "Question has been marked as inappropriate!";
                question.Body = "If you are bald, what hair color do they put on your driver's license?";
            }
            return question;
        }

        internal static Question ValidateCommand(Question question, string command)
        {
            if (command == "upvote")
            {
                question.UpVotes++;
            }
            if (command == "downvote")
            {
                question.DownVotes++;
            }
            if (command == "innappropriate")
            {
                question.Inappropriate++;
                question = ValidateIsAppropriate(question);
            }
            return question;
        }
    }
}
