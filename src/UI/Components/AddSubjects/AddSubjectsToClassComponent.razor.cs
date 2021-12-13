using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using System.Collections.Generic;
using Shared.ViewModels;
using System.Linq;

namespace UI.Components.AddSubjects
{
    public partial class AddSubjectsToClassComponent
    {
        private bool isDataLoaded = false;

        [Parameter]
        public string ClassName { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IClassHttpService ClassHttpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LocalStorageService.RemoveItemAsync("MySubjects");
            IEnumerable<StudentVm> students = await ClassHttpService.GetAllStudentsFromClass(ClassName);
            if (students.Count() == 0)
            {
                ToastService.ShowError("W tej klasie nie ma żadnych uczniów!", "Błąd");
                NavigationManager.NavigateTo("/");
                return;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && isDataLoaded)
            {
                await JSRuntime.InvokeVoidAsync("initializeSubjects");
            }
        }
    }
}