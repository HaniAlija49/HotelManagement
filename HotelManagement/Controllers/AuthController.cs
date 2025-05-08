using AutoMapper;
using HotelManagement.DTOs.Responses;
using HotelManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public AuthController(
            IUserRepository userRepository,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            JwtService jwtService)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DTOs.Requests.RegisterRequest request)
        {
            if (await _userRepository.GetByEmailAsync(request.Email) is not null)
                return BadRequest("User already exists.");

            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userRepository.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userRepository.AddToRoleAsync(user, RoleNames.Customer);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOs.Requests.LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null) return Unauthorized("Invalid credentials.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid credentials.");

            var roles = await _userRepository.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user, roles);

            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = (await _userRepository.GetRolesAsync(user)).ToList();

            return Ok(userDto);
        }
    }
}