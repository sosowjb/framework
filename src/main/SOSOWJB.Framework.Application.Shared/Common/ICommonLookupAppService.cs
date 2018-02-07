using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SOSOWJB.Framework.Common.Dto;
using SOSOWJB.Framework.Editions.Dto;

namespace SOSOWJB.Framework.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}