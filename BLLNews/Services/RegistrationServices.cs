using AutoMapper;
using Microsoft.AspNetCore.Identity;
using News.BLL.DataTransferObjects.UserDto;
using News.BLL.Identity;
using News.BLL.Interfaces;
using News.DAL.Models;
using System.Threading.Tasks;
namespace News.BLL.Services
{
    /// <summary>
    /// the service class that serves the register controller
    /// </summary>
    public class RegistrationServices : IRegistrationServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IMapper _mapper;
        public RegistrationServices(UserManager<User> userManager, IAuthenticationManager authManager, IMapper mapper)
        {
            _userManager = userManager;
            _authManager = authManager;
            _mapper = mapper;
        }
        public async Task<bool> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                return false;
            }
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return true;
        }
    }
}
