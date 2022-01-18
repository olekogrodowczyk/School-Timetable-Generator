using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services.ErrorModels
{
    public class ErrorModel
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public string[] Errors { get; set; } = new string[] { };

        public void Clear()
        {
            ErrorMessage = string.Empty;
            Errors = new string[] { };
        }
    }
}
