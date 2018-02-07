using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Configuration;

namespace SOSOWJB.Framework.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);

        List<NameValueDto> GetWindowsTimezones();
    }
}
