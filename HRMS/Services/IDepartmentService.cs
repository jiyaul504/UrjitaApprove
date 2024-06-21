using HRMS.EntityDto;
namespace HRMS.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(DepartmentDto departmentDto);
        Task UpdateDepartmentAsync(DepartmentDto departmentDto);
        Task DeleteDepartmentAsync(int id);
    }
}
