using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Entities
{
    public class Topic
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int QuestionId { get; set; }

        public ICollection<QuestionTopics> Questions { get; set; }
    }
}
