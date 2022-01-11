using Microsoft.AspNetCore.Components;
using Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services.Interfaces;

namespace UI.Components.Plans
{
    public partial class PlanComponent : ComponentBase
    {

        private IEnumerable<TimetableOutcomeVm> outcomeData = new List<TimetableOutcomeVm>();

        [Parameter]
        public string TimetableId { get; set; }

        [Inject]
        public ITimetableHttpService TimetableHttpService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Refresh();
        }

        private async Task Refresh()
        {
            outcomeData = await TimetableHttpService.GetAlgorithmOutcome(int.Parse(TimetableId));
            StateHasChanged();
        }
    }
}
