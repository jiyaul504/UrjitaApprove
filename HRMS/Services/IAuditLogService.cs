using HRMS.EntityDto;
using System.Threading.Tasks;

public interface IAuditLogService
{
    Task LogActionAsync(string action, string username, string details);
    Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync();

}
