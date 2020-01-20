using Abp.AutoMapper;
using Akh.Breed.Editions;
using Akh.Breed.MultiTenancy.Payments.Dto;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.SubscriptionManagement
{
    [AutoMapTo(typeof(ExecutePaymentDto))]
    public class PaymentResultViewModel : SubscriptionPaymentDto
    {
        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
