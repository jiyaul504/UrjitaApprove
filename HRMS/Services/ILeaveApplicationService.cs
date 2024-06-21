using HRMS.EntityDto;

namespace HRMS.Services
{
    public interface ILeaveApplicationService
    {
        Task<IEnumerable<LeaveApplicationDto>> GetAllLeaveApplicationsAsync();
        Task<LeaveApplicationDto> GetLeaveApplicationByIdAsync(int id);
        Task CreateLeaveApplicationAsync(LeaveApplicationDto leaveApplicationDto);
        Task UpdateLeaveApplicationAsync(LeaveApplicationDto leaveApplicationDto);
        Task DeleteLeaveApplicationAsync(int id);
    }

}
