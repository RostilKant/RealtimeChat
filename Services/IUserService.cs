using System.Threading.Tasks;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Services
{
    public interface IUserService
    {
        public Task<bool> RegisterUser(UserForRegistrationDto userForRegistration, ModelStateDictionary modelState);
    }
}