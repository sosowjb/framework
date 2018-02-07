using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.MultiTenancy;
using Shouldly;
using SOSOWJB.Framework.Authorization;
using SOSOWJB.Framework.Authorization.Users;
using Xunit;

namespace SOSOWJB.Framework.Tests.Authorization.Users
{
    public class UserAppService_Unlock_Tests : UserAppServiceTestBase
    {
        private readonly UserManager _userManager;
        private readonly LogInManager _loginManager;

        public UserAppService_Unlock_Tests()
        {
            _userManager = Resolve<UserManager>();
            _loginManager = Resolve<LogInManager>();

            CreateTestUsers();
        }

        [Fact]
        public async Task Should_Unlock_User()
        {
            //Arrange

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
            var user = await GetUserByUserNameAsync("jnash");

            //Pre conditions
            ShouldBeBooleanExtensions.ShouldBeFalse((await _userManager.IsLockedOutAsync(user)));
            ShouldBeBooleanExtensions.ShouldBeTrue(user.IsLockoutEnabled);

            //Try wrong password until lockout
            AbpLoginResultType loginResultType;
            do
            {
                loginResultType = (await _loginManager.LoginAsync(user.UserName, "wrong-password", AbpTenantBase.DefaultTenantName)).Result;
            } while (loginResultType != AbpLoginResultType.LockedOut);

            ShouldBeBooleanExtensions.ShouldBeTrue((await _userManager.IsLockedOutAsync(await GetUserByUserNameAsync("jnash"))));

            //Act

            await UserAppService.UnlockUser(new EntityDto<long>(user.Id));

            //Assert

            ShouldBeTestExtensions.ShouldBe((await _loginManager.LoginAsync(user.UserName, "wrong-password", AbpTenantBase.DefaultTenantName)).Result, AbpLoginResultType.InvalidPassword);
        }
    }
}