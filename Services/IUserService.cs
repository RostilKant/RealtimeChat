using System.Threading.Tasks;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Services
{
    public interface IUserService
    {
        public User User { get; }
        public Task<bool> RegisterUser(UserForRegistrationDto userForRegistration, ModelStateDictionary modelState);

        public Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto);

        Task<string> CreateToken();
    }
}