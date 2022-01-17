using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using UI.Services.ErrorModels;
using UI.Services.Exceptions;

namespace UI.Components
{
    public static class ComponentRequestHandler
    {
        private static ErrorModel errorModel { get; set; }

        public static async Task<bool> HandleRequest<T>(Func<T, Task> action, T value, IToastService toastService)
        {
            errorModel = new ErrorModel();
            bool isError = false;
            try
            {
                await action(value);
            }
            catch (ApiException e)
            {
                isError = true;
                errorModel.ErrorMessage = e.ErrorResult.Message;
                errorModel.Errors = e.ErrorResult.Errors;
            }
            catch (Exception e)
            {
                isError = true;
                errorModel.ErrorMessage = e.Message;
            }
            if (errorModel.ErrorMessage != String.Empty) { toastService.ShowError(errorModel.ErrorMessage, "Błąd"); }
            if (errorModel.Errors != null) 
            {
                foreach (string error in errorModel.Errors)
                {
                    toastService.ShowError(error);
                }
            }
            errorModel.Clear();
            return isError;
        }
    }
}