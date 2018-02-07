using System.Threading.Tasks;
using Abp.Application.Services;
using SOSOWJB.Framework.Configuration.Host.Dto;

namespace SOSOWJB.Framework.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
