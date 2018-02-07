using Abp.AspNetZeroCore;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using SOSOWJB.Framework.Configuration;
using SOSOWJB.Framework.EntityFrameworkCore;
using SOSOWJB.Framework.Migrator.DependencyInjection;

namespace SOSOWJB.Framework.Migrator
{
    [DependsOn(typeof(FrameworkEntityFrameworkCoreModule))]
    public class FrameworkMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public FrameworkMigratorModule(FrameworkEntityFrameworkCoreModule frameworkEntityFrameworkCoreModule)
        {
            frameworkEntityFrameworkCoreModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(FrameworkMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                FrameworkConsts.ConnectionStringName
                );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameworkMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}