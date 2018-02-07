using System.Threading.Tasks;
using Abp.Application.Services;
using SOSOWJB.Framework.Sessions.Dto;

namespace SOSOWJB.Framework.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
