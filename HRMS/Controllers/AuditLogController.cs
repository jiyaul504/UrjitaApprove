// Controllers/AuditLogController.cs
using HRMS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRMS.Controllers
{
    public class AuditLogController : Controller
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _auditLogService.GetAllAuditLogsAsync();
            return View(logs);
        }
    }
}
