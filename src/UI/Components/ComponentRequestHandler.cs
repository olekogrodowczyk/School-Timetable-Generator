using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using UI.Services.Exceptions;

namespace UI.Components
{
    public static class ComponentRequestHandler
    {             
        public static async Task HandleRequest<T>
            (Func<T, Task> action, T value, string errorMessage, string[] errors, IToastService toastService)
        {
            try
            {
                await action(value);
            }
            catch (ApiException e)
            {
                errorMessage = e.ErrorResult.Message;
                errors = e.ErrorResult.Errors;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            if (errorMessage != String.Empty) { toastService.ShowError(String.Empty, errorMessage); }
            if (errors != null)
            {
                foreach (string error in errors)
                {
                    toastService.ShowError(error);
                }
            }
        }
    }
}
