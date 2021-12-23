using BLLNews.DataTransferObjects.UserDto;
using BLLNews.Interfaces;
using Contract;
using DALNews.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace APINews.Controllers
{
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthenticationManager _authManager;
        private readonly IRegistrationServices _registration;
        public RegistrationController(ILoggerManager logger, IRegistrationServices registration, IAuthenticationManager authManager)
        {
            _logger = logger;
            _registration = registration;
            _authManager = authManager;
        }
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userForRegistration"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            try
            {
                var result = await _registration.RegisterUser(userForRegistration);
                if (result == false)
                {
                    return BadRequest();
                }
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(RegisterUser)} action {ex}, {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
