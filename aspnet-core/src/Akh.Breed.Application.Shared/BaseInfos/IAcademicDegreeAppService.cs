using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IAcademicDegreeAppService : IApplicationService
    {
        Task<PagedResultDto<AcademicDegreeListDto>> GetAcademicDegree(GetAcademicDegreeInput input);
        Task<AcademicDegreeCreateOrUpdateInput> GetAcademicDegreeForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateAcademicDegree(AcademicDegreeCreateOrUpdateInput input);
        Task DeleteAcademicDegree(EntityDto input);
    }
}