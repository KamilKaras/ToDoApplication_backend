using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ToDoAplication.Tools;

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
        public async Task<IActionResult> UsersList(int page, int count)
        {

            var usersList = await _userManager.Users.Skip((page - 1) * count).Take(count).ToListAsync();
            var usersToSend = usersList.Select(user => Mapper.PrepareToSend(user)).ToList();

            return Ok(usersToSend);
        }
        [HttpDelete("{id}")]
        public  async Task<IActionResult> DeleteUser(string id)
        {
            var userToDelete =  _userManager.Users.FirstOrDefault(i => i.Id == id);
            if(userToDelete is null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(userToDelete);
            return Ok(userToDelete);

        }
    }
}
