using System.Threading.Tasks;
using Shouldly;
using SOSOWJB.Framework.Sessions;
using Xunit;

namespace SOSOWJB.Framework.Tests.Sessions
{
    public class SessionAppService_Tests : AppTestBase
    {
        private readonly ISessionAppService _sessionAppService;

        public SessionAppService_Tests()
        {
            _sessionAppService = Resolve<ISessionAppService>();
        }

        [MultiTenantFact]
        public async Task Should_Get_Current_User_When_Logged_In_As_Host()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act
            var output = await _sessionAppService.GetCurrentLoginInformations();
            
            //Assert
            var currentUser = await GetCurrentUserAsync();
            output.User.ShouldNotBe(null);
            ShouldBeStringTestExtensions.ShouldBe(output.User.Name, currentUser.Name);
            ShouldBeStringTestExtensions.ShouldBe(output.User.Surname, currentUser.Surname);

            output.Tenant.ShouldBe(null);
        }

        [Fact]
        public async Task Should_Get_Current_User_And_Tenant_When_Logged_In_As_Tenant()
        {
            //Act
            var output = await _sessionAppService.GetCurrentLoginInformations();

            //Assert
            var currentUser = await GetCurrentUserAsync();
            var currentTenant = await GetCurrentTenantAsync();

            output.User.ShouldNotBe(null);
            ShouldBeStringTestExtensions.ShouldBe(output.User.Name, currentUser.Name);

            output.Tenant.ShouldNotBe(null);
            ShouldBeStringTestExtensions.ShouldBe(output.Tenant.Name, currentTenant.Name);

            output.Application.Version.ShouldBe(AppVersionHelper.Version);
            output.Application.ReleaseDate.ShouldBe(AppVersionHelper.ReleaseDate);
        }
    }
}
