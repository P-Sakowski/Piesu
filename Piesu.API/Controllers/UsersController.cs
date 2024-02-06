using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Piesu.API.Data;
using Piesu.API.Helpers;
using Piesu.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Piesu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        protected readonly IMapper _mapper;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            ILogger<AuthController> logger,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            IMapper mapper)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._httpContext = httpContextAccessor;
            this._configuration = configuration;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get()
        {
            var dbUsers = _userManager.Users;
            var users = new List<UserDto>();
            foreach (var dbUser in dbUsers)
            {
                var user = _mapper.Map<UserDto>(dbUser);
                var dbRoles = await _userManager.GetRolesAsync(dbUser);
                user.Roles = dbRoles.ToArray();
                users.Add(user);
            }
            return users;
        }

        [HttpGet]
        [Authorize]
        [Route("current")]
        public async Task<ApplicationUser> GetUserById()
        {
            var userEmail = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            return user;
        }

        [HttpPost]
        [Authorize]
        [Route("current/profile-image")]
        public async Task<IActionResult> SetProfileImage([FromForm] IFormFile image)
        {
            var userEmail = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid user." });
            }

            var fileHelper = new FileHelper(_configuration);
            user.ProfileImageURL = await fileHelper.UploadImageAsync(image);
            await _userManager.UpdateAsync(user);

            return Ok(new { Message = "Profile image uploaded succesfully." });
        }
    }
}
