using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Exceptions
{
     //TODO: implement AppException
    public class ConflictException  : Exception
    {
        
        public ConflictException(string message) : base(message) { }
        
    }
}
