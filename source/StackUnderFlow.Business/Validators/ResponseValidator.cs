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

        public static Response ValidateResponseChanges(Response realRes, Response submitRes)
        {
            if (realRes.UpVotes != submitRes.UpVotes)
            {
                realRes.UpVotes++;
            }
            if (realRes.DownVotes != submitRes.DownVotes)
            {
                realRes.DownVotes++;
            }
            if (realRes.Inappropriate != submitRes.Inappropriate)
            {
                realRes.Inappropriate++;
                realRes = ValidateIsAppropriate(realRes);
            }
            return realRes;
        }
    }
}
