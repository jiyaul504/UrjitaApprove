using HRMS.Data;
using HRMS.EntityDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _auditLogRepository;

    public AuditLogService(IAuditLogRepository auditLogRepository)
    {
        _auditLogRepository = auditLogRepository;
    }

    public async Task LogActionAsync(string action, string username, string details)
    {
        var auditLog = new AuditLogDto
        {
            Action = action,
            Username = username,
            Details = details,
            Timestamp = DateTime.UtcNow
        };
        await _auditLogRepository.CreateAuditLogAsync(auditLog);
    }

    public async Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync()
    {
        return await _auditLogRepository.GetAllAuditLogsAsync();
    }
}
