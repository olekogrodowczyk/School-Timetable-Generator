using Application.Dto.LoginUserVm;
using Application.Dto.RegisterUserVm;
using Application.Responses;
using System.Threading.Tasks;

namespace UI.Interfaces
{
    public interface IAuthenticationHttpService
    {
        Task<Result<string>> LoginUser(LoginUserDto model);
        public Task<Result<int>> RegisterUser(RegisterUserDto model);
    }
}
