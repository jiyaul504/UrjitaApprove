// Services/AppSettingService.cs
using HRMS.Data;
using HRMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services
{
    public class AppSettingService : IAppSettingService
    {
        private readonly ApplicationDbContext _context;

        public AppSettingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppSetting>> GetAllSettingsAsync()
        {
            return await _context.AppSettings.ToListAsync();
        }

        public async Task<AppSetting> GetSettingByIdAsync(int id)
        {
            return await _context.AppSettings.FindAsync(id);
        }

        public async Task CreateSettingAsync(AppSetting setting)
        {
            _context.AppSettings.Add(setting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSettingAsync(AppSetting setting)
        {
            _context.Entry(setting).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSettingAsync(int id)
        {
            var setting = await _context.AppSettings.FindAsync(id);
            _context.AppSettings.Remove(setting);
            await _context.SaveChangesAsync();
        }
    }

    
}
