using ArticalesApp.Data;
using ArticalesApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArticalesApp.Controllers
{

    public class UserController : Controller
    {
        readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;

        public UserController(ApplicationDbContext context,UserManager<User> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {

            await _context.User.ToListAsync();

            var users = await _context.User.ToListAsync();

            foreach (var user in users)
            {
                user.UserRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            }
            return View(users);
        }
    }
}





