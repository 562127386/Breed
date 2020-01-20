using System.Threading.Tasks;
using Abp.Application.Services;
using Akh.Breed.Install.Dto;

namespace Akh.Breed.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}
