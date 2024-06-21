using Microsoft.AspNetCore.Identity;

namespace HRMS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AppUserName { get; set; }
        public string RoleName { get; set; }
    }
}
