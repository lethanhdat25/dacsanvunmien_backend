using dacsanvungmien.Models;
using dacsanvungmien.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Controllers
{
    public abstract class BaseController:ControllerBase
    {
        protected IAccountRepository users;
        public BaseController(IAccountRepository usersRepo)
        {
            this.users = usersRepo;
        }
        [Authorize]
        protected async Task<Account> GetCurrentUser()
        {
            var id = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
            var user = await users.GetUserByIdAsync(id);
            return user;
        }
    }
}
