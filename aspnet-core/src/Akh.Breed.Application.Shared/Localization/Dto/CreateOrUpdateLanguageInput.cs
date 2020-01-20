using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}
