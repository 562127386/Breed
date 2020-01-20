using System.Threading.Tasks;
using Abp.Application.Services;
using Akh.Breed.Configuration.Tenants.Dto;

namespace Akh.Breed.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}

