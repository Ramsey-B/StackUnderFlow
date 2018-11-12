using StackUnderFlow.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Business.Validators
{
    internal static class ResponseValidator
    {
        public static Response ValidateIsAppropriate(Response response)
        {
            if (response.Inappropriate > 5)
            {
                response.Body = "I'm not a very helpful person";
            }
            return response;
        }

        public static Response ValidateCommand(Response response, string command)
        {
            if (command == "upvote")
            {
                response.UpVotes++;
            }
            if (command == "downvote")
            {
                response.DownVotes++;
            }
            if (command == "innappropriate")
            {
                response.Inappropriate++;
                response = ValidateIsAppropriate(response);
            }
            return response;
        }
    }
}
