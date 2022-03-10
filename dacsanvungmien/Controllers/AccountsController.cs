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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using Newtonsoft.Json.Serialization;

namespace dacsanvungmien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        public IConfiguration _configuration;
        public IUserRepository repository;
        private readonly ILogger<ExternalLoginModel> _logger;
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public AccountsController(
            IConfiguration configuration,
            IAccountRepository users,
            ILogger<ExternalLoginModel> logger,
            IUserRepository repository):base(users)
        {
            _logger = logger;
            _configuration = configuration;
            this.repository = repository;
        }

        // Post: api/Accounts
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AccountDto>> Register(RegisterDto form)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }
            if (form.Roles == null)
            {
                form.Roles = "USER";
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
                Roles=form.Roles,
                PhoneNumber=form.PhoneNumber,
                UserAddress=form.UserAddress
            };
            await users.AddUserAsync(user);
            var token = CreateAccessToken(user);
            // tra token ve client
            return user.AsDto(token);
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
                new Claim("role",user.Roles),
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

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await repository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await repository.DeleteUserAsync(id);
            return NoContent();
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutUser(int id, Account account)
        {

            var user = await repository.GetUserByIdAsync(id);
            if (user is null)
            {
                return NotFound();
    }
            if (user.AccountPassword == account.AccountPassword)
            {
                user.FaceBook = account.FaceBook!=null?account.FaceBook:user.FaceBook;
                user.Roles = account.Roles!=null ? account.Roles : user.Roles;
                user.Gmail = account.Gmail != null ? account.Gmail : user.Gmail;
                user.UserAddress = account.UserAddress != null ? account.UserAddress : user.UserAddress;
                user.PhoneNumber = account.PhoneNumber != null ? account.PhoneNumber : user.PhoneNumber;
                user.Name = account.Name != null ? account.Name : user.Name;
                await repository.SaveChangesAsync();
                return user.AsDto();
            }
            return NoContent();
        }

        // GET: api/Accounts
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return (await repository.GetUsersAsync()).Select(item => item.AsDto());
        }

        // GET: api/Accounts/5
        [HttpGet("{params}")]
        public async Task<ActionResult<UserDto>> GetUser(int id, string gmail = null)
        {
            return await (gmail != null ? GetUserByGmail(gmail) : GetUserById(id));
        }

        private async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await repository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user.AsDto();
        }
        private async Task<ActionResult<UserDto>> GetUserByGmail(string gmail)
        {
            var user = await repository.GetUserByEmailAsync(gmail);

            if (user == null)
            {
                return NotFound();
            }

            return user.AsDto();
        }

    }
}
