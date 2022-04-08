using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class AuthService
    {
        private static readonly string _secret = Guid.NewGuid().ToString();
        public static byte[] Key = Encoding.Default.GetBytes(_secret);


        public string Authenticate(string login, string password)
        {
            if (login != "Admin" || password != "passw0rd")
            {
                return null;
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "SuperAdmin"),
                new Claim(ClaimTypes.Role, "Read"),
                new Claim(ClaimTypes.Role, "Write"),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = new ClaimsIdentity(claims);
            tokenDescriptor.Expires = DateTime.Now.AddMinutes(1);
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task Authenticate(HttpContext context, string user, string password)
        {
            if (user != "Admin" || password != "passw0rd")
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.DateOfBirth, DateTime.Now.AddYears(-19).ToShortDateString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }
    }
}
