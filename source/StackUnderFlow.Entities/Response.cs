using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderFlow.Entities
{
    public class Response
    {
        public int Id { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public bool Solution { get; set; } = false;
        public int Inappropriate { get; set; } = 0;
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpVotes { get; set; } = 0;
        public int DownVotes { get; set; } = 0;
        public Question Question { get; set; }
    }
}
