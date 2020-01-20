using Abp.Events.Bus;

namespace Akh.Breed.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}
