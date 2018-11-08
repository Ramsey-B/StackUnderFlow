using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Entities
{
    public class QuestionTopics
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int TopicId { get; set; }
        public Question Question { get; set; }
        public Topic Topic { get; set; }
    }
}
