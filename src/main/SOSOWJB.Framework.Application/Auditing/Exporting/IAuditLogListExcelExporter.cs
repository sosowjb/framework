using System.Collections.Generic;
using SOSOWJB.Framework.Auditing.Dto;
using SOSOWJB.Framework.Dto;

namespace SOSOWJB.Framework.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
