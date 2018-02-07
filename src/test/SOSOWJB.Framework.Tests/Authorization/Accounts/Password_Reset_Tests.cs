using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Shouldly;
using SOSOWJB.Framework.Authorization.Accounts;
using SOSOWJB.Framework.Authorization.Accounts.Dto;
using SOSOWJB.Framework.Authorization.Users;
using Xunit;

namespace SOSOWJB.Framework.Tests.Authorization.Accounts
{
    public class Password_Reset_Tests : AppTestBase
    {
        [Fact]
        public async Task Should_Reset_Password()
        {
            //Arrange

            var user = await GetCurrentUserAsync();

            string passResetCode = null;

            var fakeUserEmailer = Substitute.For<IUserEmailer>();
            fakeUserEmailer.SendPasswordResetLinkAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(callInfo =>
            {
                var calledUser = callInfo.Arg<User>();
                ShouldBeStringTestExtensions.ShouldBe(calledUser.EmailAddress, user.EmailAddress);
                passResetCode = calledUser.PasswordResetCode; //Getting the password reset code sent to the email address
                return Task.CompletedTask;
            });

            LocalIocManager.IocContainer.Register(Component.For<IUserEmailer>().Instance(fakeUserEmailer).IsDefault());
            
            var accountAppService = Resolve<IAccountAppService>();

            //Act

            await accountAppService.SendPasswordResetCode(
                new SendPasswordResetCodeInput
                {
                    EmailAddress = user.EmailAddress
                }
            );

            await accountAppService.ResetPassword(
                new ResetPasswordInput
                {
                    Password = "newpass",
                    ResetCode = passResetCode,
                    UserId = user.Id
                }
            );

            //Assert

            user = await GetCurrentUserAsync();
            LocalIocManager
                .Resolve<IPasswordHasher<User>>()
                .VerifyHashedPassword(user, user.Password, "newpass")
                .ShouldBe(PasswordVerificationResult.Success);
        }
    }
}