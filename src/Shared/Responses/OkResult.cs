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

    public class Result<T> : OkResult
    {
        public T Value { get; set; }

        public Result()
        {
            Success = true;
        }

        public Result(T value)
        {
            Success = true;
            Value = value;
        }

        public Result(T value, string message)
        {
            Success = true;
            Value = value;
            Message = message;
        }
    }
}
