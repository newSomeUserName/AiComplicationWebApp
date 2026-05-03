using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Exceptions
{

    //TODO: implement class
    public abstract class AppException : Exception
    {
        public abstract int StatusCode { get; }
        public AppException(string message): base(message) { }
    }
}
