using Abp.Application.Services;
using SOSOWJB.Framework.Dto;
using SOSOWJB.Framework.Logging.Dto;

namespace SOSOWJB.Framework.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
