using System.Threading.Tasks;
using Abp.Application.Services;

namespace Akh.Breed.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}

