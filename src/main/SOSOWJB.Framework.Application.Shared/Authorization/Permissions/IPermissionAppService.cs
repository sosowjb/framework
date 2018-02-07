using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SOSOWJB.Framework.Authorization.Permissions.Dto;

namespace SOSOWJB.Framework.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
