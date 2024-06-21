using HRMS.EntityDto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models;
using HRMS.Data;

namespace HRMS.Services
{
    public class LeaveApplicationService : ILeaveApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LeaveApplicationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeaveApplicationDto>> GetAllLeaveApplicationsAsync()
        {
            var leaveApplications = await _context.LeaveApplications
            .Include(l => l.Approver)
            .Include(l => l.Employee)
            .ToListAsync();
            return _mapper.Map<List<LeaveApplicationDto>>(leaveApplications);
        }


        public async Task<LeaveApplicationDto> GetLeaveApplicationByIdAsync(int id)
        {
            var leaveApplication = await _context.LeaveApplications
        .Include(l => l.Employee) // Include the Employee entity
        .FirstOrDefaultAsync(l => l.LeaveApplicationId == id); // Find by ID

            return _mapper.Map<LeaveApplicationDto>(leaveApplication);
        }

        public async Task CreateLeaveApplicationAsync(LeaveApplicationDto leaveApplicationDto)
        {
            var leaveApplication = _mapper.Map<LeaveApplication>(leaveApplicationDto);
            leaveApplication.Status = "Pending"; // Default status
            _context.LeaveApplications.Add(leaveApplication);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLeaveApplicationAsync(LeaveApplicationDto leaveApplicationDto)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(leaveApplicationDto.LeaveApplicationId);
            if (leaveApplication != null)
            {
                leaveApplication.EmployeeId = leaveApplicationDto.EmployeeId;
                leaveApplication.EmployeeName = leaveApplicationDto.EmployeeName;
                leaveApplication.StartDate = leaveApplicationDto.StartDate;
                leaveApplication.EndDate = leaveApplicationDto.EndDate;
                leaveApplication.Reason = leaveApplicationDto.Reason;
                leaveApplication.Status = leaveApplicationDto.Status;
                leaveApplication.FullDay = leaveApplicationDto.FullDay;
                leaveApplication.HalfDay = leaveApplicationDto.HalfDay;
                leaveApplication.TypeOfLeave = leaveApplicationDto.TypeOfLeave;

                _context.LeaveApplications.Update(leaveApplication);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteLeaveApplicationAsync(int LeaveApplicationId)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(LeaveApplicationId);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
                await _context.SaveChangesAsync();
            }
        }
    }
}
