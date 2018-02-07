using System.Threading.Tasks;
using Abp.Application.Services;
using SOSOWJB.Framework.Configuration.Tenants.Dto;

namespace SOSOWJB.Framework.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
