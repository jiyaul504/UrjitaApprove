namespace HRMS.EntityDto
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public DateTime DateOfJoining { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmergencyContact { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } // This is to display department name directly
    }
}

