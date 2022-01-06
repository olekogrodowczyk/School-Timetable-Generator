using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Interfaces;

namespace UI.Services.Services
{
    public class GroupHttpService : IGroupHttpService
    {
        private readonly IHttpService _httpService;

        public GroupHttpService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task DeleteGroupWithAssignments(int groupId)
        {
            var result = await _httpService.Delete<OkResult>($"api/group?groupId={groupId}", null);
        }
    }
}
