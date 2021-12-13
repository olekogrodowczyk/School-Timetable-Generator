using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services.Interfaces;

namespace UI.Components.AddSubjects
{
    public partial class AddSubjectsComponent
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

        protected async override Task OnInitializedAsync()
        {
            isBusy = true;
            classessNames = await ClassHttpService.GetAllClassessNames();
            isBusy = false;
        }



    }
}
