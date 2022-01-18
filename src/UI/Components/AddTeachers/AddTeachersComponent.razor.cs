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
        private bool isInvalid = false;
        private IEnumerable<TeacherVm> teachersCreated = new List<TeacherVm>();
        private Dictionary<int, string> styles = new Dictionary<int, string>();

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public ITeacherHttpService TeacherHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ITimetableStateHttpService TimetableStateHttpService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await PhaseGuard();
            await LocalStorageService.RemoveItemAsync("MyTeachers");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initializeAddTeachers");
                await Refresh();
            }          
        }

        private async Task Refresh()
        {
            await LocalStorageService.RemoveItemAsync("TeacherToAdd");
            isInvalid = false;
            teachersCreated = await TeacherHttpService.GetAllTeachersFromTimetable();
            await InitializeStyles();
            await Task.Delay(50);
            StateHasChanged();           
        }

        private async Task UpdateTeacher()
        {
            string teacherToUpdateString = await LocalStorageService.GetItemAsync<string>("TeacherToEdit");
            var teacherToUpdate = await JsonDeserializer.DeserializeValue<TeacherModel>(teacherToUpdateString, ToastService);
            isInvalid = await ComponentRequestHandler.HandleRequest(TeacherHttpService.UpdateTeacherWithAvailabilities, teacherToUpdate, ToastService);

            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie zaktualizowano wybranego nauczyciela"); }
            await Refresh();
        }

        protected async Task PhaseGuard()
        {
            int currentTimetable = await TimetableStateHttpService.GetCurrentTimetable();
            int currentPhase = await TimetableStateHttpService.GetCurrentPhase(currentTimetable);
            if (currentPhase != 1)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        private Task InitializeStyles()
        {
            styles.Clear();
            foreach(var teacher in teachersCreated)
            {
                styles.Add(teacher.Id, String.Empty);
            }
            return Task.CompletedTask;
        }

        protected void ChangeStyle(int teacherId)
        {            
            styles[teacherId] = styles[teacherId] == String.Empty ? "max-height: 378px;" :  String.Empty;
        }

        protected async Task AddTeacher()
        {
            string teacherToAddString = await LocalStorageService.GetItemAsync<string>("TeacherToAdd");
            if (teacherToAddString is null) { return; }
            var teacherToAdd = await JsonDeserializer.DeserializeValue<TeacherModel>(teacherToAddString, ToastService);
            bool teacherExists = await CheckIfTeacherExists(teacherToAdd.imie, teacherToAdd.nazwisko);
            if (teacherExists) { return; }

            isInvalid = await ComponentRequestHandler.HandleRequest(TeacherHttpService.UpdateTeacherWithAvailabilities, teacherToAdd, ToastService);
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie dodano wybranego nauczyciela"); }            
            await Refresh();
        }

        public async Task<bool> CheckIfTeacherExists(string firstName, string lastName)
        {
            bool teacherExists = await TeacherHttpService.TeacherExists(firstName, lastName);
            if (teacherExists)
            {
                ToastService.ShowError($"Podany nauczyciel - {firstName} {lastName} już istnieje");
                return true;
            }
            return false;
        }

        public async Task DeleteTeacher(int teacherId)
        {
            isInvalid = await ComponentRequestHandler.HandleRequest<int>(TeacherHttpService.DeleteTeacher, teacherId, ToastService);
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie usunięto wybraną salę"); }
            await Refresh();
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