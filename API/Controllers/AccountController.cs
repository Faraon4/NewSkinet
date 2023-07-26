using System.Security.Claims;
using API.Dtos;
using API.Errors;
using API.Extensions;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            

            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            return new UserDto 
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Displayname = user.DisplayName
            };
        }

        [HttpPost("login")]
        // UserDto -> what information will be retured after the succesful login -> we not using AppUser type because it contains too much information
        // LoginDto -> WHat information is needed for logging
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); //false at the end is the flag lockoutonfailure -> brutforsce attack prevention -> if we put true, we need to specify nr of attempts, we put false, because we do not need it

            if(!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto 
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Displayname = user.DisplayName
            };
        }


        //Check if an e-mail address is alreayd in use
        //This can be a helper method , that can be used on client side for validation
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
        
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<Address>> GetUserAddress()
        {
            

            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
            return user.Address;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser 
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            // we return Badrequest, not smth else
            if(!result.Succeeded) return BadRequest(new ApiResponse(400));
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Displayname = user.DisplayName
            };
        }
    }
}