using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace Akh.Breed.Authorization.Accounts.Dto
{
    public class SendPasswordResetCodeInput
    {
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}
