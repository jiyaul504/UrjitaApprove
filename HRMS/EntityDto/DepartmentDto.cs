using HRMS.EntityDto;

namespace HRMS.EntityDto
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EmployeeDto> Employees { get; set; }
    }
}


