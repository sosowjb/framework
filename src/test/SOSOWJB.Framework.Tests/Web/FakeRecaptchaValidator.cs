using System.Threading.Tasks;
using SOSOWJB.Framework.Security.Recaptcha;

namespace SOSOWJB.Framework.Tests.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
