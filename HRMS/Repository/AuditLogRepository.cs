using AutoMapper;
using HRMS.EntityDto;
using HRMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRMS.Data;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AuditLogRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CreateAuditLogAsync(AuditLogDto auditLogDto)
    {
        var auditLog = _mapper.Map<AuditLog>(auditLogDto);
        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync()
    {
        var auditLogs = await _context.AuditLogs.ToListAsync();
        return _mapper.Map<IEnumerable<AuditLogDto>>(auditLogs);
    }
}
