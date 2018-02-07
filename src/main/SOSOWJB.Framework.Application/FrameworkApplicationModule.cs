using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SOSOWJB.Framework.Authorization;

namespace SOSOWJB.Framework
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(FrameworkCoreModule)
        )]
    public class FrameworkApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameworkApplicationModule).GetAssembly());
        }
    }
}