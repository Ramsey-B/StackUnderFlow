using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackUnderFlow.Entities
{
    public class Question
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public bool Answered { get; set; }
        public int Inappropriate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public ICollection<QuestionTopics> Topics { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
    }
}
