using Microsoft.Extensions.Configuration;

namespace SOSOWJB.Framework.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
