using StackUnderFlow.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Business
{
    public class TopicsService
    {
        private readonly DataContext _context;

        public TopicsService(DataContext context)
        {
            _context = context;
        }
    }
}
