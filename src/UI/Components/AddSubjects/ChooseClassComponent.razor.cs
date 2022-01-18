using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services.Interfaces;

namespace UI.Components.AddSubjects
{
    public partial class ChooseClassComponent
    {
        private IEnumerable<string> classessNames;
        private bool isBusy = false;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public IClassHttpService ClassHttpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ITimetableStateHttpService TimetableStateHttpService { get; set; }

        [Inject]
        public ITimetableHttpService TimetableHttpService { get; set;}


        protected override async Task OnInitializedAsync()
        {         
            isBusy = true;
            await PhaseGuard();
            classessNames = await ClassHttpService.GetAllClassessNames();
            isBusy = false;
        }

        protected async Task PhaseGuard()
        {
            int currentTimetable = await TimetableStateHttpService.GetCurrentTimetable();
            int currentPhase = await TimetableStateHttpService.GetCurrentPhase(currentTimetable);
            if (currentPhase != 3 && currentPhase != 2)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        protected async Task GenerateTimetable()
        {
            isBusy = true;
            await TimetableHttpService.Generate();
            isBusy = false;
            ToastService.ShowSuccess("Pomyślnie wygenerowano plan lekcji");
            int currentTimetableId = await TimetableHttpService.GetCurrentUserTimetableId();
            NavigationManager.NavigateTo($"plans/{currentTimetableId}");
        }

        
    }
}