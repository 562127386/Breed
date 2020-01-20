using System.Threading.Tasks;
using Abp.Application.Services;
using Akh.Breed.Configuration.Host.Dto;

namespace Akh.Breed.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}

