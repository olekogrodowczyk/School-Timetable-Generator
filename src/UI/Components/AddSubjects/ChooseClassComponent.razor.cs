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

        protected override async Task OnInitializedAsync()
        {
            isBusy = true;
            classessNames = await ClassHttpService.GetAllClassessNames();
            isBusy = false;
        }

        protected async Task GenerateTimetable()
        {
            await TimetableHttpService.Generate();
        }

        [Inject]
        public ITimetableHttpService TimetableHttpService { get; set; }
    }
}