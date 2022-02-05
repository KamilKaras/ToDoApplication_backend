using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAplication.Models;

namespace ToDoAplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
       
        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult UsersList()
        {
            
            var usersList = _userManager.Users.ToList();
            var usersToSend = usersList.Select(user => PrepareToSend(user)).ToList();

            return Ok(usersToSend);
        }
        private UserToSend PrepareToSend(IdentityUser user)
        {
            var newUser = new UserToSend();
            newUser.Email = user.Email;
            newUser.UserName = user.UserName;
            newUser.Id = user.Id;
            newUser.Tasks = new List<string>();
            return newUser;
        }
    }
}
