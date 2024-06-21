using AutoMapper;
using HRMS.Data;
using HRMS.EntityDto;
using HRMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.Include(e => e.Department)
                                                   .FirstOrDefaultAsync(e => e.EmployeeId == id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<EmployeeDto> GetManagerByDepartmentIdAsync(int departmentId)
        {
            // List of manager titles to match
            var managerTitles = new List<string> { "HR Manager", "Operations Manager", "Sales Manager", "Project Manager", "Marketing Manager", "Product Manager", "Business Development Manager" };

            var manager = await _context.Employees
                .Where(e => e.DepartmentId == departmentId && managerTitles.Contains(e.Position))
                .FirstOrDefaultAsync();

            if (manager == null)
            {
                // Log the error
                Console.WriteLine($"No manager found for department ID: {departmentId}");

                // Optional: Fallback logic if no manager is found
                var fallbackApprover = await _context.Employees
                    .Where(e => managerTitles.Contains(e.Position))
                    .FirstOrDefaultAsync();

                if (fallbackApprover != null)
                {
                    Console.WriteLine($"Fallback manager found with ID: {fallbackApprover.EmployeeId}");
                    return _mapper.Map<EmployeeDto>(fallbackApprover);
                }

                // If no fallback is desired, return null
                return null;
            }

            return _mapper.Map<EmployeeDto>(manager);
        }



        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments.ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }
    }
}
