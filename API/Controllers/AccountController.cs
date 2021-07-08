using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;

        public AccountController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        IMapper mapper,
        IUserService userService,
        ITransactionService transactionService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _transactionService = transactionService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //kod iz felipe - udemy
           // var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
           // var user = await _userManager.FindByEmailAsync(email);
           // var userId = user.Id;

            var user = await _userManager.FindByEmailFromClaimsPrinciple(User);

            var userId = user.Id;

            
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                RoleName = await _userService.RoleName(userId)
            };
        }
        [Authorize]
        [HttpGet("gethim")]
        public async Task<ActionResult<UserDto1>> GetCurrentUser1()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(User);

            var userId = user.Id;
            
            return new UserDto1
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                UserId = userId,
                DisplayName = user.DisplayName,
                RoleName = await _userService.RoleName(userId)
            };
        }
        
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery]string email)
        {             
             return await _userManager.FindByEmailAsync(email) != null;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            if (user.LockoutEnd != null)
            {
               return Unauthorized(new ApiResponse(401));
            }

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                RoleName = await _userService.RoleName(user.Id)
            };
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
             if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult
                (new ApiValidationErrorResponse{Errors = new []{"Email address is in use"}});
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                SurtaxId = 1
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            // ovo si dodao, pripazi - za sada ne šljaka!
            await _transactionService.InitializeTaxLiability(user.Email);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
                //RoleName = "Participant"
            };
        }
    }
}





