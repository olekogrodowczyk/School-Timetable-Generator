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
        private bool error;
        private Dictionary<int, string> styles = new Dictionary<int, string>();
        private IEnumerable<SubjectVm> subjectsCreated = new List<SubjectVm>();
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
        public IGroupHttpService GroupHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public ITeacherHttpService TeacherHttpService { get; set; }

        

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Refresh();
            }
        }

        private async Task Refresh()
        {
            subjectsCreated = await SubjectHttpService.GetAllSubjectsWithGroups(ClassName);
            await InitializeStyles();
            await Task.Delay(50);
            StateHasChanged();
        }

        private Task InitializeStyles()
        {
            styles.Clear();
            foreach (var classModel in subjectsCreated)
            {
                styles.Add(classModel.Id, String.Empty);
            }
            return Task.CompletedTask;
        }

        protected void ChangeStyle(int classId)
        {
            int maxHeight = 427;
            styles[classId] = styles[classId] == String.Empty ? $"max-height: {maxHeight.ToString()}px;" : String.Empty;
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

        protected async Task DeleteGroup(int groupId)
        {
            error = await ComponentRequestHandler.HandleRequest<int>(GroupHttpService.DeleteGroupWithAssignments
                , groupId, _errorMessage, _errors, ToastService);
            if (!error)
            {
                ToastService.ShowSuccess("Pomyślnie usunięto wybraną grupę");
            }
            await Refresh();
        }

        protected async Task AddSubject()
        {
            value = await LocalStorageService.GetItemAsync<string>("SubjectToAdd");
            SubjectModel subjectToAdd = null;
            try
            {
                subjectToAdd = JsonConvert.DeserializeObject<SubjectModel>(value);
            }
            catch (Exception ex)
            {
                ToastService.ShowError("Nastąpił problem z serializacją danych");
            }
            if (await ValidateData(subjectToAdd)) { return; }
            if (_errorMessage != String.Empty) { ToastService.ShowError(String.Empty, _errorMessage); }
            try
            {
                await SubjectHttpService.AddSubjectWithGroups(subjectToAdd, ClassName);
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
            if (_errors != null)
            {
                foreach (string error in _errors)
                {
                    ToastService.ShowError(error);
                }
            }
            if (_errorMessage == String.Empty) { ToastService.ShowSuccess("Pomyślnie zapisano dane"); }
            await Refresh();
        }

        private async Task<bool> ValidateData(SubjectModel subjectToAdd)
        {
            bool error = false;
            foreach (var item in subjectToAdd.groupSubjectList)
            {
                if(item.name == null || item.teacher == null || item.hours == null)
                {
                    ToastService.ShowError("Podano puste dane");
                    error = true;
                }
                if(item.hours == null || int.Parse(item.hours) <0)
                {
                    ToastService.ShowError("Podano nieprawidłową liczbę godzin");
                    error = true;
                }
                var teacherNames = item.teacher.Split(" ");
                if (teacherNames.Length != 2)
                {
                    ToastService.ShowError("Podano nieprawidłowe dane nauczyciela");
                    error = true;
                }
                bool teacherExists = await TeacherHttpService.TeacherExists(teacherNames[0], teacherNames[1]);
                if (!teacherExists) 
                { 
                    ToastService.ShowError($"Podany nauczyciel - {teacherNames[0]} {teacherNames[1]} nie istnieje");
                    error = true;
                }
            }
            return error;
        }

        private async Task DeleteSubject(int subjectId)
        {
            error = await ComponentRequestHandler.HandleRequest<int>(SubjectHttpService.DeleteSubjectWithGroups
                , subjectId, _errorMessage, _errors, ToastService);
            if (!error)
            {
                ToastService.ShowSuccess("Pomyślnie usunięto wybrany przedmiot");
            }
            await Refresh();
        }

    }
}