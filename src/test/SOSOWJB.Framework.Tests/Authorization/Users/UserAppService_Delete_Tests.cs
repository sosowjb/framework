using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Shouldly;
using Xunit;

namespace SOSOWJB.Framework.Tests.Authorization.Users
{
    public class UserAppService_Delete_Tests : UserAppServiceTestBase
    {
        [Fact]
        public async Task Should_Delete_User()
        {
            //Arrange
            CreateTestUsers();

            var user = await GetUserByUserNameOrNullAsync("artdent");
            ShouldBeTestExtensions.ShouldNotBe(user, null);

            //Act
            await UserAppService.DeleteUser(new EntityDto<long>(user.Id));

            //Assert
            user = await GetUserByUserNameOrNullAsync("artdent");
            ShouldBeTestExtensions.ShouldBe(user.IsDeleted, true);
        }
    }
}