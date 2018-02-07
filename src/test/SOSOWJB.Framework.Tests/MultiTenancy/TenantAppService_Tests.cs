using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Notifications;
using Abp.Zero.Configuration;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SOSOWJB.Framework.MultiTenancy;
using SOSOWJB.Framework.MultiTenancy.Dto;
using SOSOWJB.Framework.Notifications;

namespace SOSOWJB.Framework.Tests.MultiTenancy
{
    public class TenantAppService_Tests : AppTestBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantAppService_Tests()
        {
            LoginAsHostAdmin();
            _tenantAppService = Resolve<ITenantAppService>();
        }

        [MultiTenantFact]
        public async Task GetTenants_Test()
        {
            //Act
            var output = await _tenantAppService.GetTenants(new GetTenantsInput());

            //Assert
            output.TotalCount.ShouldBe(1);
            output.Items.Count.ShouldBe(1);
            output.Items[0].TenancyName.ShouldBe(Tenant.DefaultTenantName);
        }

        [MultiTenantFact]
        public async Task Create_Update_And_Delete_Tenant_Test()
        {
            //CREATE --------------------------------

            //Act
            await _tenantAppService.CreateTenant(
                new CreateTenantInput
                {
                    TenancyName = "testTenant",
                    Name = "Tenant for test purpose",
                    AdminEmailAddress = "admin@testtenant.com",
                    AdminPassword = "123qwe",
                    IsActive = true
                });

            //Assert
            var tenant = await GetTenantOrNullAsync("testTenant");
            ShouldBeTestExtensions.ShouldNotBe(tenant, null);
            ShouldBeStringTestExtensions.ShouldBe(tenant.Name, "Tenant for test purpose");
            ShouldBeTestExtensions.ShouldBe(tenant.IsActive, true);

            await UsingDbContextAsync(tenant.Id, async context =>
            {
                //Check static roles
                var staticRoleNames = Resolve<IRoleManagementConfig>().StaticRoles.Where(r => r.Side == MultiTenancySides.Tenant).Select(role => role.RoleName).ToList();
                foreach (var staticRoleName in staticRoleNames)
                {
                    (await EntityFrameworkQueryableExtensions.CountAsync(context.Roles, r => r.TenantId == tenant.Id && r.Name == staticRoleName)).ShouldBe(1);
                }

                //Check default admin user
                var adminUser = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Users, u => u.TenantId == tenant.Id && u.UserName == AbpUserBase.AdminUserName);
                ShouldBeNullExtensions.ShouldNotBeNull(adminUser);

                //Check notification registration
                (await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<NotificationSubscriptionInfo>(context.NotificationSubscriptions, ns => ns.UserId == adminUser.Id && ns.NotificationName == AppNotificationNames.NewUserRegistered)).ShouldNotBeNull();
            });

            //GET FOR EDIT -----------------------------

            //Act
            var editDto = await _tenantAppService.GetTenantForEdit(new EntityDto(tenant.Id));

            //Assert
            editDto.TenancyName.ShouldBe("testTenant");
            editDto.Name.ShouldBe("Tenant for test purpose");
            editDto.IsActive.ShouldBe(true);

            // UPDATE ----------------------------------

            editDto.Name = "edited tenant name";
            editDto.IsActive = false;
            await _tenantAppService.UpdateTenant(editDto);

            //Assert
            tenant = await GetTenantAsync("testTenant");
            ShouldBeStringTestExtensions.ShouldBe(tenant.Name, "edited tenant name");
            ShouldBeTestExtensions.ShouldBe(tenant.IsActive, false);

            // DELETE ----------------------------------

            //Act
            await _tenantAppService.DeleteTenant(new EntityDto((await GetTenantAsync("testTenant")).Id));

            //Assert
            ShouldBeTestExtensions.ShouldBe((await GetTenantOrNullAsync("testTenant")).IsDeleted, true);
        }
    }
}
