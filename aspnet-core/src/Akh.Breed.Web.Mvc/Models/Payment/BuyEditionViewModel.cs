using System.Collections.Generic;
using Akh.Breed.Editions;
using Akh.Breed.Editions.Dto;
using Akh.Breed.MultiTenancy.Payments;
using Akh.Breed.MultiTenancy.Payments.Dto;

namespace Akh.Breed.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}

