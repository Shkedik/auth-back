using Services.Contracts.Dtos;
using System;
using System.Threading.Tasks;

namespace Services.Contracts.Services
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterDto registerDto);

        Task<bool> Login(LoginDto loginDto);

        Task<UserDto> GetUserByEmail(string email);
    }
}
