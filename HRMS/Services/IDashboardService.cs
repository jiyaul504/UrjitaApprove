using HRMS.Models;
using System.Threading.Tasks;

namespace HRMS.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}
