using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; } = 0;

        [Required]
        [StringLength(100)]
        [Display(Name ="Full Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="DOB")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        public string Position { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Joining")]
        public DateTime DateOfJoining { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Phone]
        public string EmergencyContact { get; set; }

        // Foreign Key
        [Display(Name ="Department")]
        public int DepartmentId { get; set; }

        // Navigation property
        public Department Department { get; set; }

    }
}
