using System.Threading.Tasks;
using Abp.Application.Services;
using Akh.Breed.MultiTenancy.Payments.PayPal.Dto;

namespace Akh.Breed.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}

