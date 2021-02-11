using Api.Infrastructure;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts.Dtos;
using Services.Contracts.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controller
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var loginDto = new LoginDto
                {
                    Email = loginModel.Email,
                    Password = loginModel.Password
                };

                if (await _authService.Login(loginDto))
                {
                    var user = await _authService.GetUserByEmail(loginDto.Email);
                    var response = GenerateToken(user);
                    return Ok(response);
                }
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var registerDto = new RegisterDto
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    ConfirmPasword = registerModel.ConfirmPassword
                };

                if (await _authService.Register(registerDto))
                {
                    var user = await _authService.GetUserByEmail(registerDto.Email);
                    var response = GenerateToken(user);
                    return Ok(response);
                }
            }

            return Unauthorized();
        }

        private object GenerateToken(UserDto user)
        {
            var identity = GetIdentity(user);
            var now = DateTime.UtcNow;
            var expireDate = now.Add(TimeSpan.FromMinutes(TokenModel.LIFETIME));

            var jwt = new JwtSecurityToken(
                issuer: TokenModel.ISSUER,
                audience: TokenModel.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: expireDate,
                signingCredentials: new SigningCredentials(TokenModel.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                encodeJwt,
                expireDate
            };

            return response;
        }

        private ClaimsIdentity GetIdentity(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
