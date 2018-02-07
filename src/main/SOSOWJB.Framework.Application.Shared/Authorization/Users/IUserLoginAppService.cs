using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SOSOWJB.Framework.Authorization.Users.Dto;

namespace SOSOWJB.Framework.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
