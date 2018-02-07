using System.Threading.Tasks;
using Abp.Application.Services;
using SOSOWJB.Framework.Editions.Dto;
using SOSOWJB.Framework.MultiTenancy.Dto;

namespace SOSOWJB.Framework.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}