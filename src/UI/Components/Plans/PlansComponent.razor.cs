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

        [Inject]
        public ITimetableHttpService TimetableHttpService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Refresh();
            }
        }

        private async Task Refresh()
        {
            generatedTimetables = await TimetableHttpService.GetGeneratedPlans();
            StateHasChanged();
        }
    }
}
