using API.Data;
using API.Interfaces;
using API.models;
using API.models.ViewModels;
using API.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ItokenService _tokenService;

        public AccountController(DataContext context,UserManager<ApplicationUser> userManager,ItokenService tokenService,RoleManager<IdentityRole> roleManager,SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _UserManager = userManager;
            _RoleManager = roleManager;
            _signInManager= signInManager;
            _tokenService = tokenService;

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid username or password.");
            }
            //HttpContent.Session.setString("username", user.UserName); // Store user ID in session
            var roles = await _UserManager.GetRolesAsync(user);
            var token = await _tokenService.createToken(user); // Generate JWT token
            return Ok(new
            {
                message = "Login successful",
                token = token,
                userName = user.UserName,
                roles = roles
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //check if user already exists
            if(await _UserManager.Users.AnyAsync(u => u.UserName == model.Email))
            {
                return BadRequest("User already exists with this email.");
            }
            //create new user
            var User = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name
            };
            var result = await _UserManager.CreateAsync(User, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!string.IsNullOrEmpty(model.Role))
            {
                if (await _RoleManager.RoleExistsAsync(model.Role))
                {
                    await _UserManager.AddToRoleAsync(User, model.Role);
                }
            }

            return Ok(new { message = "User created successfully", userName = User.UserName, role = model.Role });
        }


    }
}
