using System.ComponentModel.DataAnnotations;

namespace SOSOWJB.Framework.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}