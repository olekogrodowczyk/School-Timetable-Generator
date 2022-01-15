using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services.Interfaces;

namespace UI.Components.Plans
{
    public partial class PlansComponent : ComponentBase
    {
        private IEnumerable<TimetableVm> generatedTimetables = new List<TimetableVm>();
        private int currentTimetableId;

        [Inject]
        public ITimetableHttpService TimetableHttpService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Refresh();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            currentTimetableId = await TimetableHttpService.GetCurrentUserTimetableId();
        }

        private async Task Refresh()
        {
            generatedTimetables = await TimetableHttpService.GetGeneratedPlans();
            currentTimetableId = await TimetableHttpService.GetCurrentUserTimetableId();
            StateHasChanged();
        }

        private async Task ChangeTimetable(int timetableId)
        {
            await TimetableHttpService.ChangeCurrentUserTimetable(timetableId);
            ToastService.ShowSuccess("Pomyślnie zmieniono plan lekcji");
            await Refresh();
        }

        private async Task CreateNewTimetable()
        {
            await TimetableHttpService.CreateTimetable();
            ToastService.ShowSuccess("Pomyślnie utworzono nowy plan lekcji");
            await Refresh();
        }
    }
}
