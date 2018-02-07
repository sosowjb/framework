using System.Threading.Tasks;
using Abp.Application.Services;
using SOSOWJB.Framework.Install.Dto;

namespace SOSOWJB.Framework.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}