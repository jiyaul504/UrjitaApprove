using HRMS.Models;

namespace HRMS.Services
{
    public interface IAppSettingService
    {
        Task<IEnumerable<AppSetting>> GetAllSettingsAsync();
        Task<AppSetting> GetSettingByIdAsync(int id);
        Task CreateSettingAsync(AppSetting setting);
        Task UpdateSettingAsync(AppSetting setting);
        Task DeleteSettingAsync(int id);
    }
}
