using System.Threading.Tasks;

namespace SOSOWJB.Framework.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
