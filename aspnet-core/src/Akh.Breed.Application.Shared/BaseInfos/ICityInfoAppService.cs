using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface ICityInfoAppService : IApplicationService
    {
        Task<PagedResultDto<CityInfoListDto>> GetCityInfo(GetCityInfoInput input);
        Task<CityInfoGetForEditOutput> GetCityInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateCityInfo(CityInfoCreateOrUpdateInput input);
        Task DeleteCityInfo(EntityDto input);
    }
}