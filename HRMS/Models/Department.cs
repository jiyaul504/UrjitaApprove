using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Navigation property
        public ICollection<Employee> Employees { get; set; }

    }
}
