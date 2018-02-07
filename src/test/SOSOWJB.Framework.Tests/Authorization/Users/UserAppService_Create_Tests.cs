using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.MultiTenancy;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SOSOWJB.Framework.Authorization.Roles;
using SOSOWJB.Framework.Authorization.Users.Dto;
using Xunit;

namespace SOSOWJB.Framework.Tests.Authorization.Users
{
    public class UserAppService_Create_Tests : UserAppServiceTestBase
    {
        [MultiTenantFact]
        public async Task Should_Create_User_For_Host()
        {
            LoginAsHostAdmin();

            await CreateUserAndTestAsync("jnash", "John", "Nash", "jnsh2000@testdomain.com", null);
            await CreateUserAndTestAsync("adams_d", "Douglas", "Adams", "adams_d@gmail.com", null, StaticRoleNames.Host.Admin);
        }

        [Fact]
        public async Task Should_Create_User_For_Tenant()
        {
            var defaultTenantId = (await GetTenantAsync(AbpTenantBase.DefaultTenantName)).Id;
            await CreateUserAndTestAsync("jnash", "John", "Nash", "jnsh2000@testdomain.com", defaultTenantId);
            await CreateUserAndTestAsync("adams_d", "Douglas", "Adams", "adams_d@gmail.com", defaultTenantId, StaticRoleNames.Tenants.Admin);
        }

        [Fact]
        public async Task Should_Not_Create_User_With_Duplicate_Username_Or_EmailAddress()
        {
            //Arrange
            CreateTestUsers();

            //Act
            await Assert.ThrowsAsync<UserFriendlyException>(
                async () =>
                    await UserAppService.CreateOrUpdateUser(
                        new CreateOrUpdateUserInput
                        {
                            User = new UserEditDto
                                   {
                                       EmailAddress = "john@nash.com",
                                       Name = "John",
                                       Surname = "Nash",
                                       UserName = "jnash", //Same username is added before (in CreateTestUsers)
                                       Password = "123qwe"
                                   },
                            AssignedRoleNames = new string[0]
                        }));
        }

        private async Task CreateUserAndTestAsync(string userName, string name, string surname, string emailAddress, int? tenantId, params string[] roleNames)
        {
            //Arrange
            AbpSession.TenantId = tenantId;

            //Act
            await UserAppService.CreateOrUpdateUser(
                new CreateOrUpdateUserInput
                {
                    User = new UserEditDto
                    {
                        EmailAddress = emailAddress,
                        Name = name,
                        Surname = surname,
                        UserName = userName,
                        Password = "123qwE*"
                    },
                    AssignedRoleNames = roleNames
                });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                //Get created user
                var createdUser = await EntityFrameworkQueryableExtensions.Include(context.Users, u => u.Roles).FirstOrDefaultAsync(u => u.UserName == userName);
                ShouldBeTestExtensions.ShouldNotBe(createdUser, null);

                //Check some properties
                ShouldBeStringTestExtensions.ShouldBe(createdUser.EmailAddress, emailAddress);
                ShouldBeTestExtensions.ShouldBe(createdUser.TenantId, tenantId);

                //Check roles
                if (roleNames.IsNullOrEmpty())
                {
                    ShouldBeTestExtensions.ShouldBe(createdUser.Roles.Count, 0);
                }
                else
                {
                    ShouldBeTestExtensions.ShouldBe(createdUser.Roles.Count, roleNames.Length);
                    foreach (var roleName in roleNames)
                    {
                        var roleId = (await GetRoleAsync(roleName)).Id;
                        Enumerable.Any<UserRole>(createdUser.Roles, ur => ur.RoleId == roleId && ur.TenantId == tenantId).ShouldBe(true);
                    }
                }
            });
        }
    }
}
