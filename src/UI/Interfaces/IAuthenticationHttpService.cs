using Application.Dto.RegisterUserVm;
using Application.Responses;
using System.Threading.Tasks;

namespace UI.Interfaces
{
    public interface IAuthenticationHttpService
    {
        public Task<Result<int>> RegisterUser(RegisterUserDto model);
    }
}
