using System.Threading.Tasks;

namespace SOSOWJB.Framework.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}