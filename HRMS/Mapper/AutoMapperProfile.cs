using AutoMapper;
using HRMS.EntityDto;
using HRMS.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
        CreateMap<EmployeeDto, Employee>();

        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentDto, Department>();

        CreateMap<LeaveApplication, LeaveApplicationDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
            .ForMember(dest => dest.Approver, opt => opt.MapFrom(src => src.Approver != null ? new EmployeeDto
            {
                FirstName = src.Approver.FirstName,
                LastName = src.Approver.LastName
            } : null))
            .ReverseMap(); // For mapping LeaveApplicationDto back to LeaveApplication

        CreateMap<AuditLogDto, AuditLog>().ReverseMap();

        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AppUserName))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
            // Add other mappings as needed
            .ReverseMap();

        //CreateMap<RoleDto, Role>().ReverseMap();
    }
}
