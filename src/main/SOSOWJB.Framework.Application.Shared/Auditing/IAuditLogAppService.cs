using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SOSOWJB.Framework.Auditing.Dto;
using SOSOWJB.Framework.Dto;

namespace SOSOWJB.Framework.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
    }
}