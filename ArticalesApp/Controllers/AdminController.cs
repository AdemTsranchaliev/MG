using ArticalesApp.Data;
using ArticalesApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArticalesApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<User> userManager;

        public AdminController(ApplicationDbContext context, RoleManager<IdentityRole> roleMgr, UserManager<User> userManager)
        {
            _context = context;
            roleManager = roleMgr;
            this.userManager = userManager;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.CreateRoleViewModel.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createRoleViewModel = await _context.CreateRoleViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (createRoleViewModel == null)
            {
                return NotFound();
            }

            return View(createRoleViewModel);
        }

        // GET: Admin/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleName")] CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = createRoleViewModel.RoleName });
                _context.Add(createRoleViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createRoleViewModel);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createRoleViewModel = await _context.CreateRoleViewModel.FindAsync(id);
            if (createRoleViewModel == null)
            {
                return NotFound();
            }
            return View(createRoleViewModel);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleName")] CreateRoleViewModel createRoleViewModel)
        {
            if (id != createRoleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(createRoleViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreateRoleViewModelExists(createRoleViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(createRoleViewModel);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createRoleViewModel = await _context.CreateRoleViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (createRoleViewModel == null)
            {
                return NotFound();
            }

            return View(createRoleViewModel);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var createRoleViewModel = await _context.CreateRoleViewModel.FindAsync(id);
            _context.CreateRoleViewModel.Remove(createRoleViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreateRoleViewModelExists(int id)
        {
            return _context.CreateRoleViewModel.Any(e => e.Id == id);
        }
    }
}
