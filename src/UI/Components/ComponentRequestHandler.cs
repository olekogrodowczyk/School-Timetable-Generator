using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using UI.Services.Exceptions;

namespace UI.Components
{
    public static class ComponentRequestHandler
    {
        public static async Task<bool> HandleRequest<T>
            (Func<T, Task> action, T value, string errorMessage, string[] errors, IToastService toastService)
        {
            bool isError = false;
            try
            {
                await action(value);
            }
            catch (ApiException e)
            {
                isError = true;
                errorMessage = e.ErrorResult.Message;
                errors = e.ErrorResult.Errors;
            }
            catch (Exception e)
            {
                isError = true;
                errorMessage = e.Message;
            }
            if (errorMessage != String.Empty) { toastService.ShowError(errorMessage, "Błąd"); }
            if (errors != null)
            {
                foreach (string error in errors)
                {
                    toastService.ShowError(error);
                }
            }
            return isError;
        }
    }
}