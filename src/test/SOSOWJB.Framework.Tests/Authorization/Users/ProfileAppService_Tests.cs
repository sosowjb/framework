using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using SOSOWJB.Framework.Authorization.Users;
using SOSOWJB.Framework.Authorization.Users.Profile;
using SOSOWJB.Framework.Authorization.Users.Profile.Dto;
using Xunit;

namespace SOSOWJB.Framework.Tests.Authorization.Users
{
    public class ProfileAppService_Tests : AppTestBase
    {
        private readonly IProfileAppService _profileAppService;

        public ProfileAppService_Tests()
        {
            _profileAppService = Resolve<IProfileAppService>();
        }

        [Fact]
        public async Task UpdateGoogleAuthenticatorKey_Test()
        {
            var currentUser = await GetCurrentUserAsync();

            //Assert
            ShouldBeNullExtensions.ShouldBeNull<string>(currentUser.GoogleAuthenticatorKey);

            //Act
            await _profileAppService.UpdateGoogleAuthenticatorKey();

            currentUser = await GetCurrentUserAsync();

            //Assert
            ShouldBeNullExtensions.ShouldNotBeNull<string>(currentUser.GoogleAuthenticatorKey);
        }

        [Fact]
        public async Task GetUserProfileForEdit_Test()
        {
            //Act
            var output = await _profileAppService.GetCurrentUserProfileForEdit();

            //Assert
            var currentUser = await GetCurrentUserAsync();
            ShouldBeStringTestExtensions.ShouldBe(output.Name, currentUser.Name);
            ShouldBeStringTestExtensions.ShouldBe(output.Surname, currentUser.Surname);
            ShouldBeStringTestExtensions.ShouldBe(output.EmailAddress, currentUser.EmailAddress);
        }

        [Fact]
        public async Task UpdateUserProfileForEdit_Test()
        {
            //Arrange
            var currentUser = await GetCurrentUserAsync();

            //Act
            await _profileAppService.UpdateCurrentUserProfile(
                new CurrentUserProfileEditDto
                {
                    EmailAddress = "test1@test.net",
                    Name = "modified name",
                    Surname = "modified surname",
                    UserName = currentUser.UserName
                });

            //Assert
            currentUser = await GetCurrentUserAsync();
            ShouldBeStringTestExtensions.ShouldBe(currentUser.EmailAddress, "test1@test.net");
            ShouldBeStringTestExtensions.ShouldBe(currentUser.Name, "modified name");
            ShouldBeStringTestExtensions.ShouldBe(currentUser.Surname, "modified surname");
        }

        [Fact]
        public async Task ChangePassword_Test()
        {
            //Act
            await _profileAppService.ChangePassword(
                new ChangePasswordInput
                {
                    CurrentPassword = "123qwe",
                    NewPassword = "2mF9d8Ac!5"
                });

            //Assert
            var currentUser = await GetCurrentUserAsync();

            LocalIocManager
                .Resolve<IPasswordHasher<User>>()
                .VerifyHashedPassword(currentUser, currentUser.Password, "2mF9d8Ac!5")
                .ShouldBe(PasswordVerificationResult.Success);
        } 
    }
}
