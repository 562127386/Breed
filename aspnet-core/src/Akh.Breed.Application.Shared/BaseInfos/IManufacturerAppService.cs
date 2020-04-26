using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IManufacturerAppService : IApplicationService
    {
        Task<PagedResultDto<ManufacturerListDto>> GetManufacturer(GetManufacturerInput input);
        Task<ManufacturerCreateOrUpdateInput> GetManufacturerForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateManufacturer(ManufacturerCreateOrUpdateInput input);
        Task DeleteManufacturer(EntityDto input);
    }
}