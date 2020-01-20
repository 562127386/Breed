using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}
