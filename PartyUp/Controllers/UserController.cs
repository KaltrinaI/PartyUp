using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Services.AuthenticationService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService _tokenService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, TokenService tokenService, ILogger<UserController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegistrationRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid client request");
            }

            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
            {
                return BadRequest("User already exists");
            }


            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.FirstName + request.LastName,
                ImageUrl = request.ImageUrl ?? "",
                DateOfBirth = new DateTime(request.DateOfBirth.Year, request.DateOfBirth.Month, request.DateOfBirth.Day, 12,0, 0, DateTimeKind.Utc)
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            _logger.LogInformation($"User {user.UserName} registered successfully.");
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Authenticate([FromBody] AuthRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Request body is empty");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("No user found");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var accessToken = await _tokenService.CreateToken(user);
            _logger.LogInformation($"User {user.UserName} authenticated successfully.");

            return Ok(new AuthResponseDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Token = accessToken,
                UserId= user.Id,
                FirstName =user.FirstName,
                LastName = user.LastName,
                ImageUrl = user.ImageUrl
            });
        }

        [HttpPost("role")]
        public async Task<ActionResult> CreateRoles(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty");
            }

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }

            _logger.LogInformation($"Role {roleName} created successfully.");
            return Ok($"Role {roleName} created successfully.");
        }

        [HttpPost("assign")]
        public async Task<ActionResult> AssignRoleToUser(string username, string roleName)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Username and role name cannot be empty");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound($"User with username '{username}' not found.");
            }

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return NotFound($"Role '{roleName}' does not exist.");
            }

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }

            _logger.LogInformation($"Role {roleName} assigned to user {username} successfully.");
            return Ok($"Role {roleName} assigned to user {username} successfully.");
        }

        [HttpGet("username/{username}")]
        [Authorize]
        public async Task<ActionResult<UserResponseDTO>> GetUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(_mapper.Map<UserResponseDTO>(user));
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<ActionResult<UserResponseDTO>> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(_mapper.Map<UserResponseDTO>(user));
        }

        [HttpPut("{userId}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(UserRequestDTO userRequest, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

           user.FirstName = userRequest.FirstName;
           user.LastName= userRequest.LastName;
           user.ImageUrl = userRequest.ImageUrl;
           user.UserName = userRequest.FirstName+userRequest.LastName;
           user.DateOfBirth = userRequest.DateOfBirth;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User updated successfully");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(_mapper.Map<IEnumerable<UserResponseDTO>>(users));
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Password reset successfully");
        }

        [HttpDelete("{userId}")]
        [Authorize]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User deleted successfully");
        }
    }
}
