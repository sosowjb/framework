using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Shouldly;
using SOSOWJB.Framework.Authorization.Roles;
using Xunit;

namespace SOSOWJB.Framework.Tests.Authorization.Users
{
    public class UserAppService_GetUserForEdit_Tests : UserAppServiceTestBase
    {
        [MultiTenantFact]
        public async Task Should_Work_For_NonExisting_User()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act
            var output = await UserAppService.GetUserForEdit(new NullableIdDto<long>());

            //Assert
            output.User.Id.ShouldBe(null);
            output.User.Name.ShouldBe(null);
            output.User.Password.ShouldBe(null);

            output.Roles.Length.ShouldBe(1);
            output.Roles.Any(r => r.RoleName == StaticRoleNames.Host.Admin).ShouldBe(true);
            output.Roles.Single(r => r.RoleName == StaticRoleNames.Host.Admin).IsAssigned.ShouldBe(true);
        }

        [Fact]
        public async Task Should_Work_For_Existing_User()
        {
            //Arrange
            var adminUser = await GetUserByUserNameOrNullAsync(AbpUserBase.AdminUserName);
            var managerRole = CreateRole("Manager");
            var roleCount = UsingDbContext<int>(context => Queryable.Count(context.Roles, r => r.TenantId == AbpSession.TenantId));

            //Act
            var output = await UserAppService.GetUserForEdit(new NullableIdDto<long> { Id = adminUser.Id });

            //Assert
            output.User.Id.ShouldBe(adminUser.Id);
            ShouldBeStringTestExtensions.ShouldBe(output.User.Name, adminUser.Name);
            output.User.Password.ShouldBe(null);

            output.Roles.Length.ShouldBe(roleCount);
            var managerRoleDto = output.Roles.FirstOrDefault(r => r.RoleName == managerRole.Name);
            managerRoleDto.ShouldNotBe(null);
            managerRoleDto.RoleId.ShouldBe(managerRole.Id);
            managerRoleDto.IsAssigned.ShouldBe(false);

            var adminRoleDto = output.Roles.FirstOrDefault(r => r.RoleName == StaticRoleNames.Tenants.Admin);
            adminRoleDto.ShouldNotBe(null);
            adminRoleDto.IsAssigned.ShouldBe(true);
        }

        protected Role CreateRole(string roleName)
        {
            return UsingDbContext(context => context.Roles.Add(new Role(AbpSession.TenantId, roleName, roleName)).Entity);
        }
    }
}
