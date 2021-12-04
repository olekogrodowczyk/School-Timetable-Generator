using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Responses;
using System.Linq;

namespace WebAPI
{
    public class CustomValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                ErrorResult errorResult = new ErrorResult()
                {
                    Success = false,
                    Errors = errors.ToArray(),
                    Message = "Błąd walidacji danych",
                };

                context.Result = new JsonResult(errorResult)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
