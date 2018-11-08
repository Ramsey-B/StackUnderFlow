using StackUnderFlow.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Business.Validators
{
    internal static class QuestionValidator
    {
        public static Question ValidateIsAppropriate(Question question)
        {
            if (question.Inappropriate > 5)
            {
                question.Title = "Question has been marked as inappropriate!";
                question.Body = "If you are bald, what hair color do they put on your driver's license?";
                question.Topics = new List<QuestionTopics>();
            }
            return question;
        }

        public static Question ValidateQuestionChanges(Question realQ, Question submitQ)
        {
            if (realQ.UpVotes != submitQ.UpVotes)
            {
                realQ.UpVotes++;
            }
            if (realQ.DownVotes != submitQ.DownVotes)
            {
                realQ.DownVotes++;
            }
            if (realQ.Inappropriate != submitQ.Inappropriate)
            {
                realQ.Inappropriate++;
                realQ = ValidateIsAppropriate(realQ);
            }
            return realQ;
        }
    }
}
