using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IDataProtector _protector;

        public User User { get; private set; }

        public UserService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IDataProtectionProvider provider)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _protector = provider.CreateProtector("SensitiveData");
        }

        public async Task<bool> RegisterUser(UserForRegistrationDto userForRegistration, 
            ModelStateDictionary modelState)
        {
            userForRegistration.PhoneNumber = _protector.Protect(userForRegistration.PhoneNumber);
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.TryAddModelError(error.Code, error.Description);
                }

                return false;
            }

            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return true;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto)
        {
            
            User = await _userManager.FindByEmailAsync(userForAuthenticationDto.Email);
            User.PhoneNumber = _protector.Unprotect(User.PhoneNumber);
            return (User != null && await _userManager.CheckPasswordAsync(User, userForAuthenticationDto.Password));
        }

        public async Task<string> CreateToken()
        {
            var credentials = SigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(credentials, claims);
            
            return $"{new JwtSecurityTokenHandler().WriteToken(tokenOptions)}";
        }

        private SigningCredentials SigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.UserName)
            };
            var roles = await _userManager.GetRolesAsync(User);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials
            signingCredentials, IEnumerable<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                jwtSettings.GetSection("validIssuer").Value,
                jwtSettings.GetSection("validAudience").Value,
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}