﻿namespace HRMS.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public string Action { get; set; }
        public string Username { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
