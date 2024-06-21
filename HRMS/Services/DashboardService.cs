using HRMS.Data;
using HRMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var totalEmployees = await _context.Employees.CountAsync();
            var totalLeaveRequests = await _context.LeaveApplications.CountAsync();
            var approvedLeaveRequests = await _context.LeaveApplications.CountAsync(l => l.Status == "Approved");
            var pendingLeaveRequests = await _context.LeaveApplications.CountAsync(l => l.Status == "Pending");
            var rejectedLeaveRequests = await _context.LeaveApplications.CountAsync(l => l.Status == "Rejected");

            return new DashboardViewModel
            {
                TotalEmployees = totalEmployees,
                TotalLeaveRequests = totalLeaveRequests,
                ApprovedLeaveRequests = approvedLeaveRequests,
                PendingLeaveRequests = pendingLeaveRequests,
                RejectedLeaveRequests = rejectedLeaveRequests,
                // Populate other metrics as needed
            };
        }
    }
}
