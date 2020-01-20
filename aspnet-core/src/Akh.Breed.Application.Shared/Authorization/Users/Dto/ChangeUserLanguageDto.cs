using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}

