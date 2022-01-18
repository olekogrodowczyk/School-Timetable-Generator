using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Shared.Dto.CreateClassroomDto;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services.ErrorModels;
using UI.Services.Interfaces;
using UI.Services.Models;

namespace UI.Components.AddClassrooms
{
    public partial class AddClassroomsComponent
    {
        private bool isInvalid = false;        
        private List<ClassroomModel> deserializedValue = new List<ClassroomModel>();
        private IEnumerable<ClassroomVm> classroomsCreated = new List<ClassroomVm>();

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IClassroomHttpService ClassroomHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public ITimetableStateHttpService TimetableStateHttpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await PhaseGuard();
            await LocalStorageService.RemoveItemAsync("MyClassrooms");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {   
            if (firstRender)
            {           
                await JSRuntime.InvokeVoidAsync("initializeAddClassrooms");
                await Refresh();
            }
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

        private async Task Refresh()
        {
            await LocalStorageService.RemoveItemAsync("ClassroomToAdd");
            isInvalid = false;
            classroomsCreated = await ClassroomHttpService.GetAllClassroomsCreated();
            StateHasChanged();
        }
        
        
        protected async Task AddClassroom()
        {
            string classroomToAddString = await LocalStorageService.GetItemAsync<string>("ClassroomToAdd");
            if(classroomToAddString is null) { return; }
            var classroomToAdd = await JsonDeserializer.DeserializeValue<ClassroomModel>(classroomToAddString, ToastService);
            isInvalid = await ComponentRequestHandler.HandleRequest(ClassroomHttpService.CreateClassroom, classroomToAdd, ToastService);          
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie dodano nową salę"); }
            await Refresh();
        }

        protected async Task UpdateClassroom()
        {
            string classroomToEditString = await LocalStorageService.GetItemAsync<string>("ClassroomToEdit");
            var classroomToEdit =  await JsonDeserializer.DeserializeValue<ClassroomModel>(classroomToEditString, ToastService);
            isInvalid = await ComponentRequestHandler.HandleRequest(ClassroomHttpService.UpdateClassroom, classroomToEdit, ToastService);       
          
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie zaktualizowano wybraną salę"); }
            await Refresh();
        }

        protected async Task DeleteClassroom(int classroomId)
        {
            isInvalid = await ComponentRequestHandler.HandleRequest(ClassroomHttpService.DeleteClassroom, classroomId, ToastService);
            if (!isInvalid) { ToastService.ShowSuccess("Pomyślnie usunięto wybraną salę"); }
            await Refresh();
        }
    }
}