using Microsoft.EntityFrameworkCore;
using StackUnderFlow.Entities;
using System;

namespace StackUnderFlow.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<QuestionTopics> QuestionTopics { get; set; }

        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }
    }
}
