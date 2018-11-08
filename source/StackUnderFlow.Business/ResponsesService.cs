using StackUnderFlow.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderFlow.Business
{
    public class ResponsesService
    {
        private readonly DataContext _context;

        public ResponsesService(DataContext context)
        {
            _context = context;
        }
    }
}
