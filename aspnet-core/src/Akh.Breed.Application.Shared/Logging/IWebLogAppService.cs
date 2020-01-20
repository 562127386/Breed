using Abp.Application.Services;
using Akh.Breed.Dto;
using Akh.Breed.Logging.Dto;

namespace Akh.Breed.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}

