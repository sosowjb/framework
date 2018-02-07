using System.Threading.Tasks;
using Abp.Application.Services;

namespace SOSOWJB.Framework.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task UpgradeTenantToEquivalentEdition(int upgradeEditionId);
    }
}
