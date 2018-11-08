using StackUnderFlow.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Business
{
    public class CommentsService
    {
        private readonly DataContext _context;

        public CommentsService(DataContext context)
        {
            _context = context;
        }
    }
}
