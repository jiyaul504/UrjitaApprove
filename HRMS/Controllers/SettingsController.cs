
using HRMS.Models;
using HRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRMS.Controllers
{
    //[Authorize(Policy = "RequireAdministratorRole")]
    public class SettingsController : Controller
    {
        private readonly IAppSettingService _appSettingService;

        public SettingsController(IAppSettingService appSettingService)
        {
            _appSettingService = appSettingService;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _appSettingService.GetAllSettingsAsync();
            return View(settings);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var setting = await _appSettingService.GetSettingByIdAsync(id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppSetting setting)
        {
            if (ModelState.IsValid)
            {
                await _appSettingService.UpdateSettingAsync(setting);
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }
    }
}
