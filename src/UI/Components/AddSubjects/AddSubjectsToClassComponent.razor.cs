using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using System.Collections.Generic;
using Shared.ViewModels;
using System.Linq;
using System;
using UI.Services.Models;
using Newtonsoft.Json;
using UI.Services.Exceptions;

namespace UI.Components.AddSubjects
{
    public partial class AddSubjectsToClassComponent
    {
        protected string value = String.Empty;
        protected string _errorMessage = String.Empty;
        protected string[] _errors;
        private List<SubjectModel> deserializedValue = new List<SubjectModel>();
        private IEnumerable<StudentVm> students;

        [Parameter]
        public string ClassName { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IClassHttpService ClassHttpService { get; set; }

        [Inject]
        public ISubjectHttpService SubjectHttpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        protected async Task HandleJson()
        {
            value = await LocalStorageService.GetItemAsync<string>("MySubjects");
            Console.WriteLine(value);
            try
            {
                deserializedValue = JsonConvert.DeserializeObject<List<SubjectModel>>(value);
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Nastąpił problem z serializacją danych");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LocalStorageService.RemoveItemAsync("MySubjects");
            students = await ClassHttpService.GetAllStudentsFromClass(ClassName);
            if (students.Count() == 0)
            {
                ToastService.ShowError("W tej klasie nie ma żadnych uczniów!", "Błąd");
                NavigationManager.NavigateTo("/");
                return;
            }
            await LocalStorageService.SetItemAsync("MyStudents", students);
            await JSRuntime.InvokeVoidAsync("initializeSubjects");
        }

        protected async Task HandleAddSubjectsWithGroups()
        {
            await HandleJson();

            try
            {
                await SubjectHttpService.AddSubjectsWithGroups(deserializedValue, ClassName);
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

            if (_errorMessage == String.Empty) { ToastService.ShowSuccess("Pomyślnie zapisano dane"); }
        }
    }
}