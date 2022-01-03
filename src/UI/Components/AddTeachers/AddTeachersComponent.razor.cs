using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using UI.Services.Models;

namespace UI.Components.AddTeachers
{
    public partial class AddTeachersComponent
    {
        protected string value = String.Empty;
        protected string _errorMessage = String.Empty;
        protected string[] _errors;
        private bool error;
        private List<TeacherModel> deserializedValue = new List<TeacherModel>();
        private IEnumerable<TeacherVm> teachersCreated = new List<TeacherVm>();

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public ITeacherHttpService TeacherHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LocalStorageService.RemoveItemAsync("MyTeachers");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initializeAddTeachers");
                await GetTeachersCreated();
            }          
        }

        protected async Task GetTeachersCreated()
        {
            teachersCreated = await TeacherHttpService.GetAllTeachersFromTimetable();
            await JSRuntime.InvokeVoidAsync("clearAllSeparate");            

            foreach (var item in teachersCreated)
            {
                var newItem = new
                {
                    firstName = item.FirstName,
                    lastName = item.LastName,
                    hoursAvailability = item.HoursAvailability,
                    availabilities = transformAvailabilitiesIntoMatrix(item.Availabilities)
                };
                await JSRuntime.InvokeAsync<Task>("addNauczycielToList", newItem);
            }
            StateHasChanged();
        }

        protected async Task AddTeacher()
        {
            string teacherToAddString = await LocalStorageService.GetItemAsync<string>("TeacherToAdd");
            TeacherModel teacherToAdd = null;
            try
            {
                teacherToAdd = JsonConvert.DeserializeObject<TeacherModel>(teacherToAddString);
            }
            catch (Exception ex)
            {
                error = true;
                ToastService.ShowError("Nastąpił problem z serializacją danych");
            }
            if (error) { return; }
            error = await ComponentRequestHandler.HandleRequest<TeacherModel>(TeacherHttpService.CreateTeacherWithAvailabilities
                , teacherToAdd, _errorMessage, _errors, ToastService);
            if (!error)
            {
                ToastService.ShowSuccess("Pomyślnie dodano nowego nauczyciela");
                await GetTeachersCreated();
            }
            
        }

        private char[][] transformAvailabilitiesIntoMatrix(IEnumerable<AvailabilityVm> availabilities)
        {
            const int arraySize = 9;
            const int rowSize = 5;
            char[][] matrix = new char[arraySize][];
            for(int i=0;i<arraySize;i++)
            {
                matrix[i] = new char[rowSize];
                for (int j = 0; j < rowSize; j++)
                {
                    matrix[i][j] = '0';
                }
            }
            foreach (var item in availabilities)
            {
                const int startsAtInit = 8;
                int firstIndexer = item.StartsAt-startsAtInit;
                int secondIndexer = MatchNumberOfWeekByName(item.DayOfWeek);
                matrix[firstIndexer][secondIndexer] = '1';
            }
            return matrix;
        }

        private int MatchNumberOfWeekByName(string nameOfDay) => nameOfDay switch
        {
            "Poniedziałek" => 0,
            "Wtorek" => 1,
            "Środa" => 2,
            "Czwartek" => 3,
            "Piątek" => 4,
            _ => -1
        };

        
    }
}