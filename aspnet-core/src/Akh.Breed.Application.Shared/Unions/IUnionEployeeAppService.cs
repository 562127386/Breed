using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Unions.Dto;

namespace Akh.Breed.Unions
{
    public interface IUnionEmployeeAppService : IApplicationService
    {
        Task<PagedResultDto<UnionEmployeeListDto>> GetUnionEmployee(GetUnionEmployeeInput input);
        Task<UnionEmployeeCreateOrUpdateInput> GetUnionEmployeeForEdit(int unionInfoId, NullableIdDto<int> input);
        Task CreateOrUpdateUnionEmployee(UnionEmployeeCreateOrUpdateInput input);
        Task DeleteUnionEmployee(EntityDto input);
    }
}