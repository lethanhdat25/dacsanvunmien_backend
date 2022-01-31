using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dacsanvungmien.Models;
using dacsanvungmien.Dtos;
using dacsanvungmien.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace dacsanvungmien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        private readonly DacSanVungMienContext _context;
        public IConfiguration _configuration;

        public AccountsController(DacSanVungMienContext context, IConfiguration configuration, IAccountRepository users):base(users)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Accounts
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto form)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var userInDb = await users.GetUserByEmailAsync(form.Gmail);
            if (userInDb is not null)
            {
                return BadRequest("The email has been registered by another account!");
            }
            var user = new Account
            {
                Name=form.Name,
                Gmail = form.Gmail,
                AccountPassword = GetMD5(form.AccountPassword),
                Role=form.Role,
                PhoneNumber=form.PhoneNumber,
                UserAddress=form.UserAddress
            };
            await users.AddUserAsync(user);
            var token = CreateAccessToken(user);
            // tra token ve client
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AccountDto>> Login(LoginDto userData)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }
            userData.Password = GetMD5(userData.Password);
            var user = await users.GetUserByEmailAsync(userData.Email);
            if (user is null || user.AccountPassword != userData.Password)
            {
                return Unauthorized("Email or Password is not correct! " + user.AccountPassword + " " + userData.Password);
            }
            var token = CreateAccessToken(user);
            // tra token ve client
            return user.AsDto(token);

        }
        private string CreateAccessToken(Account user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                new Claim("Id",user.Id.ToString()),
                new Claim("PhoneNumber",user.PhoneNumber),
                new Claim("Gmail",user.Gmail),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                expires: DateTime.Now.AddDays(1), signingCredentials: signature);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] frData = Encoding.UTF8.GetBytes(str);
            byte[] toData = md5.ComputeHash(frData);
            string hashString = "";
            for (int i = 0; i < toData.Length; i++)
            {
                hashString += toData[i].ToString("x2");
            }
            return hashString;
        }
    }
}
