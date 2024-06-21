using System.ComponentModel.DataAnnotations;

namespace HRMS.EntityDto
{
    public class RoleDto
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        [StringLength(100, ErrorMessage = "Role name cannot be longer than 100 characters")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "At least one permission is required")]
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
