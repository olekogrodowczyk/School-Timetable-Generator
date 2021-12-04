using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class ErrorResult : ResultBase
    {
        public string[] Errors { get; set; }

        public ErrorResult()
        {

        }

        public ErrorResult(string message)
        {
            Message = message;
            Success = false;
        }

        public ErrorResult(string message, string[] errors)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }
    }
}
