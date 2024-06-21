using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class LeaveApplication
    {
        [Key]
        public int LeaveApplicationId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string TypeOfLeave { get; set; }
        public int ApproverId { get; set; }
        public Employee Approver { get; set; }
        public bool IsApproved { get; set; }
        public int DepartmentId { get; set; }
        public int CreatedById { get; set; }

        public Employee Employee { get; set; } // Navigation property to Employee (if it exists)

        public bool HalfDay { get; set; } // New field for Half Day
        public bool FullDay { get; set; } // New field for Full Day

      

    }

}
