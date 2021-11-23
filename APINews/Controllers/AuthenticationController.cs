using AutoMapper;
using Contract;
using Contract.Identity;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APINews.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        public AuthenticationController(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _logger = logger; _mapper = mapper; _userManager = userManager; _authManager = authManager;
        }




        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            try
            {
                if (!await _authManager.ValidateUser(user))
                {
                    _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
                    return Unauthorized();
                }

                return Ok(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(Authenticate)} action {ex}");

                return StatusCode(500, "Internal server error");
            }
        }
    }
}
