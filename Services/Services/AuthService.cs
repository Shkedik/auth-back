using Microsoft.AspNetCore.Identity;
using Models;
using Services.Contracts.Dtos;
using Services.Contracts.Services;
using System;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            var userDto = new UserDto
            {
                Email = user.Email,
                Id = user.Id
            };

            return userDto;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: true);
            
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Register(RegisterDto registerDto)
        {
            var user = new User

            {
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }

            return false;
        }
    }
}
