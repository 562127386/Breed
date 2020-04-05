using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Herds.Dto;

namespace Akh.Breed.Herds
{
    public interface IHerdGeoLogAppService : IApplicationService
    {
        Task<PagedResultDto<HerdGeoLogListDto>> GetHerdGeoLog(GetHerdGeoLogInput input);
        Task<GetHerdGeoLogForEditOutput> GetHerdGeoLogForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateHerdGeoLog(HerdGeoLogCreateOrUpdateInput input);
        Task DeleteHerdGeoLog(EntityDto input);
    }
}