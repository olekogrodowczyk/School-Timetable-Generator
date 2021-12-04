using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace UI.Components.AddTeachers
{
    public partial class AddTeachersComponent
    {
        protected string value = String.Empty;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initializeAddClass");
            }
        }

        protected async Task GetJson()
        {
            value = await LocalStorageService.GetItemAsync<string>("MyClasses");
        }
    }
}
