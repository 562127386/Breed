using System.Threading.Tasks;
using Abp.Application.Services;
using Akh.Breed.Editions.Dto;
using Akh.Breed.MultiTenancy.Dto;

namespace Akh.Breed.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}
