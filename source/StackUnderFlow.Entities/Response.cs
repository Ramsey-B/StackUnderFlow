using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Entities
{
    public class Response
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool Solution { get; set; }
        public int Inappropriate { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public Question Question { get; set; }
    }
}
