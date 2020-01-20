using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Akh.Breed.MultiTenancy.Accounting.Dto;

namespace Akh.Breed.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}

