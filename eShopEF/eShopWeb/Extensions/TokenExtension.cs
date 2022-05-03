using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopWeb.Extensions
{
    public class TokenExtension
    {
        public string Token { get; set; }

        public TokenExtension(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Auth:key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: config["Auth:issuer"],
                audience: config["Auth:audience"],
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signinCredentials
            );

            Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
