using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class OkResult : ResultBase
    {        
        public OkResult(string message)
        {
            Success = true;
            Message = message;
        }
        public OkResult()
        {
            Success = true;
        }
    }

    public class OkResult<T> : OkResult
    {
        public T Value { get; set; }

        public OkResult()
        {
            Success = true;
        }

        public OkResult(T value)
        {
            Success = true;
            Value = value;
        }

        public OkResult(T value, string message)
        {
            Success = true;
            Value = value;
            Message = message;
        }
    }
}
