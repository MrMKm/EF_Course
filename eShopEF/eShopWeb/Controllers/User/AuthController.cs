using Entities.Models;
using eShopWeb.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopWeb.Controllers.User
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            // Login logic to redirect if credentials are correct 
            var result = await _signInManager.PasswordSignInAsync
                (login.Email, login.Password, login.RememberMe, false);

            if (result.Succeeded)
            {
                return Ok(new TokenExtension(_config, _userManager).Token);
            }

            return BadRequest("Invalid credentials, try again...");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest register)
        {
            var user = new ApplicationUser(register.Email, register.Name);

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, register.Role.ToString());

                if (result.Succeeded)
                    return Ok("Now try log in...");

                else
                {
                    await _userManager.DeleteAsync(user);
                }
            }

            return BadRequest("Something went wrong, try again...");
        }

        [HttpGet]
        [Route("log-out")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok("User signed out successfully");
        }
    }
}
