using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shouldly;
using SOSOWJB.Framework.Authorization;
using SOSOWJB.Framework.Authorization.Users;
using SOSOWJB.Framework.Authorization.Users.Dto;
using Xunit;

namespace SOSOWJB.Framework.Tests.Authorization.Users
{
    public class UserAppService_PermissionEdit_Tests : UserAppServiceTestBase
    {
        [Fact]
        public async Task Should_Get_User_Permissions()
        {
            //Arrange
            var admin = await GetUserByUserNameAsync(AbpUserBase.AdminUserName);

            //Act
            var output = await UserAppService.GetUserPermissionsForEdit(new EntityDto<long>(admin.Id));
            
            //Assert
            output.GrantedPermissionNames.ShouldNotBe(null);
            output.Permissions.ShouldNotBe(null);
        }

        [Fact]
        public async Task Should_Update_User_Permissions()
        {
            //Arrange
            var admin = await GetUserByUserNameAsync(AbpUserBase.AdminUserName);
            var permissions = Resolve<IPermissionManager>()
                .GetAllPermissions()
                .Where(p => p.MultiTenancySides.HasFlag(AbpSession.MultiTenancySide))
                .ToList();
            
            //Act
            await UserAppService.UpdateUserPermissions(
                new UpdateUserPermissionsInput
                {
                    Id = admin.Id,
                    GrantedPermissionNames = permissions.Select(p => p.Name).ToList()
                });

            //Assert
            var userManager = Resolve<UserManager>();
            foreach (var permission in permissions)
            {
                ShouldBeTestExtensions.ShouldBe((await userManager.IsGrantedAsync(admin, permission)), true);
            }
        }

        [Fact]
        public async Task Should_Reset_Permissions()
        {
            //Arrange
            var admin = await GetUserByUserNameAsync(AbpUserBase.AdminUserName);
            UsingDbContext<EntityEntry<UserPermissionSetting>>(
                context => context.UserPermissions.Add(
                    new UserPermissionSetting
                    {
                        TenantId = AbpSession.TenantId,
                        UserId = admin.Id,
                        Name = AppPermissions.Pages_Administration_Roles,
                        IsGranted = false
                    }));

            //Act
            await UserAppService.ResetUserSpecificPermissions(new EntityDto<long>(admin.Id));

            //Assert
            (await UsingDbContextAsync<int>(context => EntityFrameworkQueryableExtensions.CountAsync<UserPermissionSetting>(context.UserPermissions, p => p.UserId == admin.Id))).ShouldBe(0);
        }
    }
}