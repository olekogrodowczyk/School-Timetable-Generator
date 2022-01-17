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
using UI.Services.ErrorModels;

namespace UI.Components.AddSubjects
{
    public partial class AddSubjectsToClassComponent
    {
        private bool isInvalid;
        private ErrorModel errorModel;
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
            isInvalid = false;
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
            isInvalid = await ComponentRequestHandler.HandleRequest(GroupHttpService.DeleteGroupWithAssignments, groupId, ToastService);
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie usunięto wybraną grupę"); }
            await Refresh();
        }

        protected async Task AddSubject()
        {
            
            string subjectToAddString = await LocalStorageService.GetItemAsync<string>("SubjectToAdd");
            var subjectToAdd = await JsonDeserializer.DeserializeValue<SubjectModel>(subjectToAddString, ToastService);
            errorModel = new ErrorModel();
            if (await ValidateData(subjectToAdd)) { return; }
            if (errorModel.ErrorMessage != String.Empty) { ToastService.ShowError(String.Empty, errorModel.ErrorMessage); }
            try
            {
                await SubjectHttpService.AddSubjectWithGroups(subjectToAdd, ClassName);
            }
            catch (ApiException e)
            {
                errorModel.ErrorMessage = e.ErrorResult.Message;
                errorModel.Errors = e.ErrorResult.Errors;
            }
            catch (Exception e)
            {
                errorModel.ErrorMessage = e.Message;
            }
            if (errorModel.Errors != null)
            {
                foreach (string error in errorModel.Errors)
                {
                    ToastService.ShowError(error);
                }
            }
            if (errorModel.ErrorMessage == String.Empty) { ToastService.ShowSuccess("Pomyślnie zapisano dane"); }
            errorModel.Clear();
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
                if(item.hours == null || int.Parse(item.hours) < 0)
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
            isInvalid = await ComponentRequestHandler.HandleRequest(SubjectHttpService.DeleteSubjectWithGroups, subjectId, ToastService);
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie usunięto wybrany przedmiot"); }
        }

    }
}