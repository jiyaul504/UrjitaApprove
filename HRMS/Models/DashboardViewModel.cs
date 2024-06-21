namespace HRMS.Models
{
    public class DashboardViewModel
    {
        public int TotalEmployees { get; set; }
        public int TotalLeaveRequests { get; set; }
        public int ApprovedLeaveRequests { get; set; }
        public int PendingLeaveRequests { get; set; }
        public int RejectedLeaveRequests { get; set; }
        // Add other metrics as needed
    }
}
