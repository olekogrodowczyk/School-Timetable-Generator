using Application.Dto.LoginUserVm;
using Application.Dto.RegisterUserVm;
using Shared.Responses;
using System.Threading.Tasks;

namespace UI.Services.Interfaces
{
    public interface IAuthenticationHttpService
    {
        Task LoginUser(LoginUserDto model);
        public Task RegisterUser(RegisterUserDto model);
    }
}
