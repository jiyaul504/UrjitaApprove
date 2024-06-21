using HRMS.EntityDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(EmployeeDto employeeDto);
        Task UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(int id);

        Task<EmployeeDto> GetManagerByDepartmentIdAsync(int departmentId); // New method
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(); // New method
    }
}
