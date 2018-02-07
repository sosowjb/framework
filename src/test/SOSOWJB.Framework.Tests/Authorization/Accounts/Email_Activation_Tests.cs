using System.Linq;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using NSubstitute;
using Shouldly;
using SOSOWJB.Framework.Authorization.Accounts;
using SOSOWJB.Framework.Authorization.Accounts.Dto;
using SOSOWJB.Framework.Authorization.Users;
using Xunit;

namespace SOSOWJB.Framework.Tests.Authorization.Accounts
{
    public class Email_Activation_Tests : AppTestBase
    {
        [Fact]
        public async Task Should_Activate_Email()
        {
            //Arrange

            UsingDbContext(context =>
            {
                //Set IsEmailConfirmed to false to provide initial test case
                var currentUser = Queryable.Single(context.Users, u => u.Id == AbpSession.UserId.Value);
                currentUser.IsEmailConfirmed = false;
            });

            var user = await GetCurrentUserAsync();
            ShouldBeBooleanExtensions.ShouldBeFalse(user.IsEmailConfirmed);

            string confirmationCode = null;

            var fakeUserEmailer = Substitute.For<IUserEmailer>();
            fakeUserEmailer.SendEmailActivationLinkAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(callInfo =>
            {
                var calledUser = callInfo.Arg<User>();
                ShouldBeStringTestExtensions.ShouldBe(calledUser.EmailAddress, user.EmailAddress);
                confirmationCode = calledUser.EmailConfirmationCode; //Getting the confirmation code sent to the email address
                return Task.CompletedTask;
            });

            LocalIocManager.IocContainer.Register(Component.For<IUserEmailer>().Instance(fakeUserEmailer).IsDefault());

            var accountAppService = Resolve<IAccountAppService>();

            //Act

            await accountAppService.SendEmailActivationLink(
                new SendEmailActivationLinkInput
                {
                    EmailAddress = user.EmailAddress
                }
            );

            await accountAppService.ActivateEmail(
                new ActivateEmailInput
                {
                    UserId = user.Id,
                    ConfirmationCode = confirmationCode
                }
            );

            //Assert

            user = await GetCurrentUserAsync();
            ShouldBeBooleanExtensions.ShouldBeTrue(user.IsEmailConfirmed);
        }
    }
}