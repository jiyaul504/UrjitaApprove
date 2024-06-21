using System.Threading.Tasks;
using HRMS.EntityDto;
using HRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;

        public UserController(IUserService userService, IAuditLogService auditLogService)
        {
            _userService = userService;
            _auditLogService = auditLogService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateUserAsync(userDto);
                return RedirectToAction(nameof(Index));
            }
            return View(userDto);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _userService.UpdateUserAsync(userDto);
                return RedirectToAction(nameof(Index));
            }
            return View(userDto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignRole(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            // You can retrieve roles from a service or ViewBag here
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string id, string roleName)
        {
            if (ModelState.IsValid)
            {
                await _userService.AssignRoleAsync(id, roleName);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> AuditLogs()
        {
            var logs = await _auditLogService.GetAllAuditLogsAsync();
            return View(logs);
        }
    }
}
