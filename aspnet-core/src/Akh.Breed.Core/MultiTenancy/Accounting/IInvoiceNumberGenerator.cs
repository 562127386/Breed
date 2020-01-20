using System.Threading.Tasks;
using Abp.Dependency;

namespace Akh.Breed.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}
