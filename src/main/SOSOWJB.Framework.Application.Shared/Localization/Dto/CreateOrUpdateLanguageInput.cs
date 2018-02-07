using System.ComponentModel.DataAnnotations;

namespace SOSOWJB.Framework.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}