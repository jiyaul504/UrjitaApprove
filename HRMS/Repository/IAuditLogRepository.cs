using HRMS.EntityDto;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAuditLogRepository
{
    Task CreateAuditLogAsync(AuditLogDto auditLogDto);
    Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync();
}
