using System;
using System.Threading.Tasks;
using Abp.Timing;
using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;
using SOSOWJB.Framework.Editions;
using SOSOWJB.Framework.MultiTenancy;
using SOSOWJB.Framework.MultiTenancy.Dto;
using SOSOWJB.Framework.MultiTenancy.Payments;
using SOSOWJB.Framework.MultiTenancy.Payments.Cache;
using SOSOWJB.Framework.MultiTenancy.Payments.Dto;
using Xunit;

namespace SOSOWJB.Framework.Tests.MultiTenancy
{
    public class TenantRegistrationAppService_Tests : AppTestBase
    {
        private readonly ITenantRegistrationAppService _tenantRegistrationAppService;
        private readonly ISubscriptionPaymentRepository _subscriptionPaymentRepository;

        private IPaymentCache _fakePaymentCache;
        private readonly string samplePaymentId = Guid.NewGuid().ToString();

        public TenantRegistrationAppService_Tests()
        {
            RegisterFakePaymentCache();
            _tenantRegistrationAppService = Resolve<ITenantRegistrationAppService>();
            _subscriptionPaymentRepository = Resolve<ISubscriptionPaymentRepository>();
        }

        private void RegisterFakePaymentCache()
        {
            _fakePaymentCache = Substitute.For<IPaymentCache>();
            _fakePaymentCache.GetCacheItemOrNull(SubscriptionPaymentGatewayType.Paypal, samplePaymentId).Returns(new PaymentCacheItem(SubscriptionPaymentGatewayType.Paypal, PaymentPeriodType.Monthly, samplePaymentId));
            _fakePaymentCache.GetCacheItemOrNull(SubscriptionPaymentGatewayType.Paypal, samplePaymentId).Returns(new PaymentCacheItem(SubscriptionPaymentGatewayType.Paypal, PaymentPeriodType.Annual, samplePaymentId));
            LocalIocManager.IocContainer.Register(Component.For<IPaymentCache>().Instance(_fakePaymentCache).IsDefault());
        }

        [MultiTenantFact]
        public async Task SubscriptionEndDateUtc_ShouldBe_Null_After_Free_Registration()
        {
            //Arrange
            var edition = new SubscribableEdition
            {
                DisplayName = "Free Edition"
            };

            await UsingDbContextAsync(async context =>
            {
                context.SubscribableEditions.Add(edition);
                await context.SaveChangesAsync();
            });

            //Act
            var registerResult = await _tenantRegistrationAppService.RegisterTenant(new RegisterTenantInput
            {
                EditionId = edition.Id,
                AdminEmailAddress = "admin@volosoft.com",
                AdminPassword = "123qwe",
                Name = "Volosoft",
                SubscriptionStartType = SubscriptionStartType.Free,
                TenancyName = "Volosoft"
            });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var tenant = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Tenants, t => t.Id == registerResult.TenantId);
                ShouldBeTestExtensions.ShouldNotBe(tenant, null);
                ShouldBeTestExtensions.ShouldBe(tenant.SubscriptionEndDateUtc.HasValue, false);
            });
        }

        [MultiTenantFact]
        public async Task Cannot_Register_To_Free_Edition_As_Trial()
        {
            //Arrange
            var edition = new SubscribableEdition
            {
                DisplayName = "Free Edition"
            };

            await UsingDbContextAsync(async context =>
            {
                context.SubscribableEditions.Add(edition);
                await context.SaveChangesAsync();
            });

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _tenantRegistrationAppService.RegisterTenant(new RegisterTenantInput
            {
                EditionId = edition.Id,
                AdminEmailAddress = "admin@volosoft.com",
                AdminPassword = "123qwe",
                Name = "Volosoft",
                SubscriptionStartType = SubscriptionStartType.Trial,
                TenancyName = "Volosoft"
            }));

            exception.Message.ShouldBe("Trial is not available for this edition !");
        }

        [MultiTenantFact]
        public async Task Should_Subscribe_To_Edition_As_Trial_If_Trial_Is_Available()
        {
            //Arrange
            var utcNow = Clock.Now.ToUniversalTime();
            var trialDayCount = 10;
            var edition = new SubscribableEdition
            {
                DisplayName = "Standard Edition",
                TrialDayCount = trialDayCount,
                MonthlyPrice = 9,
                AnnualPrice = 99
            };

            await UsingDbContextAsync(async context =>
            {
                context.SubscribableEditions.Add(edition);
                await context.SaveChangesAsync();
            });

            var result = await _tenantRegistrationAppService.RegisterTenant(new RegisterTenantInput
            {
                EditionId = edition.Id,
                AdminEmailAddress = "admin@volosoft.com",
                AdminPassword = "123qwe",
                Name = "Volosoft",
                SubscriptionStartType = SubscriptionStartType.Trial,
                TenancyName = "Volosoft"
            });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var tenant = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Tenants, t => t.Id == result.TenantId);
                ShouldBeTestExtensions.ShouldNotBe(tenant, null);
                ShouldBeTestExtensions.ShouldBe(tenant.SubscriptionEndDateUtc.HasValue, true);
                ShouldBeTestExtensions.ShouldBe(tenant.SubscriptionEndDateUtc.Value.Date, utcNow.Date.AddDays(trialDayCount));
            });
        }

        [MultiTenantFact]
        public async Task Monthly_Registratin_Should_Add_30_Days_To_SubscriptionEndDate()
        {
            await Paid_Registratin_Should_Add_N_Days_To_SubscriptionEndDate(PaymentPeriodType.Monthly);
        }

        [MultiTenantFact]
        public async Task Annual_Registratin_Should_Add_365_Days_To_SubscriptionEndDate()
        {
            await Paid_Registratin_Should_Add_N_Days_To_SubscriptionEndDate(PaymentPeriodType.Annual);
        }

        private async Task Paid_Registratin_Should_Add_N_Days_To_SubscriptionEndDate(PaymentPeriodType paymentPeriodType)
        {
            //Arrange
            var utcNow = Clock.Now.ToUniversalTime();
            var trialDayCount = 10;
            var edition = new SubscribableEdition
            {
                DisplayName = "Gold Edition",
                TrialDayCount = trialDayCount,
                MonthlyPrice = 19,
                AnnualPrice = 199
            };

            await UsingDbContextAsync(async context =>
            {
                context.SubscribableEditions.Add(edition);
                await context.SaveChangesAsync();
            });

            //Don't test payment here.
            _fakePaymentCache.AddCacheItem(new PaymentCacheItem(SubscriptionPaymentGatewayType.Paypal, paymentPeriodType, samplePaymentId));
            _subscriptionPaymentRepository.Insert(new SubscriptionPayment
            {
                PaymentPeriodType = paymentPeriodType,
                EditionId = edition.Id,
                Amount = edition.MonthlyPrice.Value,
                Gateway = SubscriptionPaymentGatewayType.Paypal,
                PaymentId = samplePaymentId,
                Status = SubscriptionPaymentStatus.Completed,
            });

            var result = await _tenantRegistrationAppService.RegisterTenant(new RegisterTenantInput
            {
                EditionId = edition.Id,
                AdminEmailAddress = "admin@volosoft.com",
                AdminPassword = "123qwe",
                Name = "Volosoft",
                SubscriptionStartType = SubscriptionStartType.Paid,
                TenancyName = "Volosoft",
                Gateway = SubscriptionPaymentGatewayType.Paypal,
                PaymentId = samplePaymentId
            });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var tenant = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Tenants, t => t.Id == result.TenantId);
                ShouldBeTestExtensions.ShouldNotBe(tenant, null);
                ShouldBeTestExtensions.ShouldBe(tenant.SubscriptionEndDateUtc.HasValue, true);
                ShouldBeTestExtensions.ShouldBe(tenant.SubscriptionEndDateUtc.Value.Date, utcNow.Date.AddDays((int)paymentPeriodType));
            });
        }
    }
}
