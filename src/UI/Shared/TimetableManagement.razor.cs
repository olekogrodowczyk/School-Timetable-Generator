using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Shared.Dto.CreateTimetableDto;
using System;
using System.Threading.Tasks;
using UI.Services.Exceptions;
using UI.Services.Interfaces;

namespace UI.Shared
{
    public partial class TimetableManagement
    {
        private string _errorMessage = String.Empty;
        private string[] _errors;

        [Inject]
        public ITimetableHttpService TimetableHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        private async Task CreateTimetable()
        {
            try
            {
                var model = new CreateTimetableDto { Name = "Nazwa1" };
                await TimetableHttpService.CreateTimetable(model);
            }
            catch (ApiException e)
            {
                _errorMessage = e.ErrorResult.Message;
                _errors = e.ErrorResult.Errors;
            }
            catch (Exception e)
            {
                _errorMessage = e.Message;
            }
            if (_errorMessage != String.Empty) { ToastService.ShowError(String.Empty, _errorMessage); }
            if (_errors != null)
            {
                foreach (string error in _errors)
                {
                    ToastService.ShowError(error);
                }
            }
            if (_errorMessage == String.Empty) { ToastService.ShowSuccess("Pomyślnie utworzono plan zajęć"); }
        }
    }
}
