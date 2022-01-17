using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services.Exceptions;
using UI.Services.Interfaces;
using UI.Services.Models;
using System.Linq;

namespace UI.Components.AddClass
{
    public partial class AddClassComponent : ComponentBase
    {
        private bool isInvalid = false;
        private IEnumerable<ClassVm> classessCreated = new List<ClassVm>();
        private Dictionary<int, string> styles = new Dictionary<int, string>();
        private int teachersCount;
        private bool isBusy = false;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IClassHttpService ClassHttpService { get; set; }

        [Inject]
        public ITeacherHttpService TeacherHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public IStudentHttpService StudentHttpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isBusy = true;
            teachersCount = await TeacherHttpService.GetTeachersCount();
            if (teachersCount == 0)
            {
                ToastService.ShowError("Brak nauczycieli do wyboru");
                NavigationManager.NavigateTo("/");
                return;
            }
            isBusy = false;
            await LocalStorageService.RemoveItemAsync("MyClasses");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initializeAddClass");
                await Refresh();
            }
        }

        private async Task Refresh()
        {
            isInvalid = false;
            classessCreated = await ClassHttpService.GetAllClassess();
            await InitializeStyles();
            await Task.Delay(50);
            StateHasChanged();
        }

        private Task InitializeStyles()
        {
            styles.Clear();
            foreach (var classModel in classessCreated)
            {
                styles.Add(classModel.Id, String.Empty);
            }
            return Task.CompletedTask;
        }

        protected void ChangeStyle(int classId)
        {
            int? studentCount = classessCreated.SingleOrDefault(x => x.Id == classId)?.Students.Count();
            int level = studentCount is not null ? ((int)studentCount / 3) + 1 : 0;
            int maxHeight = 106 * level;
            styles[classId] = styles[classId] == String.Empty ? $"max-height: {maxHeight.ToString()}px;" : String.Empty;
        }

        private async Task UpdateClass()
        {
            string classToEditString = await LocalStorageService.GetItemAsync<string>("ClassToEdit");
            var classToEdit = await JsonDeserializer.DeserializeValue<ClassModel>(classToEditString, ToastService);
            isInvalid = await ComponentRequestHandler.HandleRequest(ClassHttpService.UpdateClass, classToEdit, ToastService);
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie zaktualizowano wybraną klasę"); }
            await Refresh();
        }          

        private async Task DeleteStudent(int studentId)
        {
            isInvalid = await ComponentRequestHandler.HandleRequest(StudentHttpService.DeleteStudent, studentId, ToastService);
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie usunięto wybranego ucznia"); }
            await Refresh();
        }

        private async Task DeleteClass(int classId)
        {
            isInvalid = await ComponentRequestHandler.HandleRequest<int>(ClassHttpService.DeleteClass, classId, ToastService);
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie usunięto wybraną klasę"); }
            await Refresh();
        }

        private async Task<bool> CheckIfTeacherFromLocalStorageExists()
        {
            string teacherToCheck = await LocalStorageService.GetItemAsync<string>("TeacherToSelect");
            var teacherNames = teacherToCheck.Split(" ");
            if (teacherNames.Length != 2)
            {
                ToastService.ShowError("Nieprawidłowe dane nauczyciela");
                return false;
            }

            bool teacherExists = await TeacherHttpService.TeacherExists(teacherNames[0], teacherNames[1]);
            if (!teacherExists) { ToastService.ShowError("Podany nauczyciel nie istnieje"); return false; }
            await JSRuntime.InvokeVoidAsync("teacherExist");
            return true;
        }

        protected async Task AddClass()
        {
            string classToAddString = await LocalStorageService.GetItemAsync<string>("ClassToAdd");
            var classToEdit = await JsonDeserializer.DeserializeValue<ClassModel>(classToAddString, ToastService);
            isInvalid = await ComponentRequestHandler.HandleRequest(ClassHttpService.CreateClass, classToEdit, ToastService);
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie utworzono nową klasę"); }
            await Refresh();
        }
    }
}