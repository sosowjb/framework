using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using SOSOWJB.Framework.Configuration;

namespace SOSOWJB.Framework.Tests.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(FrameworkTestModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
