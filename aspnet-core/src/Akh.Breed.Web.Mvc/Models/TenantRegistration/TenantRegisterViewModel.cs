using Akh.Breed.Editions;
using Akh.Breed.Editions.Dto;
using Akh.Breed.MultiTenancy.Payments;
using Akh.Breed.Security;
using Akh.Breed.MultiTenancy.Payments.Dto;

namespace Akh.Breed.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}

