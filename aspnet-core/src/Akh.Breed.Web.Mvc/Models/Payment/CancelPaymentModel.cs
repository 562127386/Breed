using Akh.Breed.MultiTenancy.Payments;

namespace Akh.Breed.Web.Models.Payment
{
    public class CancelPaymentModel
    {
        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}
