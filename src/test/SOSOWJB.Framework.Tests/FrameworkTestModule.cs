using System;
using System.IO;
using Abp.AspNetZeroCore;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using SOSOWJB.Framework.Configuration;
using SOSOWJB.Framework.EntityFrameworkCore;
using SOSOWJB.Framework.Security.Recaptcha;
using SOSOWJB.Framework.Tests.Configuration;
using SOSOWJB.Framework.Tests.DependencyInjection;
using SOSOWJB.Framework.Tests.Url;
using SOSOWJB.Framework.Tests.Web;
using SOSOWJB.Framework.Url;

namespace SOSOWJB.Framework.Tests
{
    [DependsOn(
        typeof(FrameworkApplicationModule),
        typeof(FrameworkEntityFrameworkCoreModule),
        typeof(AbpTestBaseModule))]
    public class FrameworkTestModule : AbpModule
    {
        public FrameworkTestModule(FrameworkEntityFrameworkCoreModule frameworkEntityFrameworkCoreModule)
        {
            frameworkEntityFrameworkCoreModule.SkipDbContextRegistration = true;
        }

        public override void PreInitialize()
        {
            var configuration = GetConfiguration();

            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);
            Configuration.UnitOfWork.IsTransactional = false;

            //Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
            Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;
            
            //Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            RegisterFakeService<AbpZeroDbMigrator>();

            IocManager.Register<IAppUrlService, FakeAppUrlService>();
            IocManager.Register<IWebUrlService, FakeWebUrlService>();
            IocManager.Register<IRecaptchaValidator, FakeRecaptchaValidator>();

            Configuration.ReplaceService<IAppConfigurationAccessor, TestAppConfigurationAccessor>();
            Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            ServiceCollectionRegistrar.Register(IocManager);
        }

        private void RegisterFakeService<TService>()
            where TService : class
        {
            IocManager.IocContainer.Register(
                Component.For<TService>()
                    .UsingFactoryMethod(() => Substitute.For<TService>())
                    .LifestyleSingleton()
            );
        }

        private static IConfigurationRoot GetConfiguration()
        {
            return AppConfigurations.Get(Directory.GetCurrentDirectory(), addUserSecrets: true);
        }
    }
}
