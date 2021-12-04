using Application.Dto.LoginUserVm;
using Application.Dto.RegisterUserVm;
using Shared.Responses;
using System.Threading.Tasks;

namespace UI.Services.Interfaces
{
    public interface IAuthenticationHttpService
    {
        Task<Result<string>> LoginUser(LoginUserDto model);
        public Task<Result<int>> RegisterUser(RegisterUserDto model);
    }
}
