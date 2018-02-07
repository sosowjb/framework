using System.ComponentModel.DataAnnotations;

namespace SOSOWJB.Framework.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
