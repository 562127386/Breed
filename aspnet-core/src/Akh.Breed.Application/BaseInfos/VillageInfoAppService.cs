using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class VillageInfoAppService :  BreedAppServiceBase, IVillageInfoAppService
    {
        private readonly IRepository<VillageInfo> _villageInfoRepository;

        public VillageInfoAppService(IRepository<VillageInfo> villageInfoRepository)
        {
            _villageInfoRepository = villageInfoRepository;
        }

        public async Task<PagedResultDto<VillageInfoListDto>> GetVillageInfo(GetVillageInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var villageInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var villageInfosListDto = ObjectMapper.Map<List<VillageInfoListDto>>(villageInfos);
            return new PagedResultDto<VillageInfoListDto>(
                userCount,
                villageInfosListDto
            );
        }
        
        public async Task<VillageInfoCreateOrUpdateInput> GetVillageInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new VillageInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var villageInfo = await _villageInfoRepository.GetAsync(input.Id.Value);
                if (villageInfo != null)
                    ObjectMapper.Map<VillageInfo,VillageInfoCreateOrUpdateInput>(villageInfo,output);
            }

            return output;
        }
        
        public async Task CreateOrUpdateVillageInfo(VillageInfoCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateVillageInfoAsync(input);
            }
            else
            {
                await CreateVillageInfoAsync(input);
            }
        }
        
        public async Task DeleteVillageInfo(EntityDto input)
        {
            await _villageInfoRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateVillageInfoAsync(VillageInfoCreateOrUpdateInput input)
        {
            var villageInfo = ObjectMapper.Map<VillageInfo>(input);
            await _villageInfoRepository.UpdateAsync(villageInfo);
        }
        
        private async Task CreateVillageInfoAsync(VillageInfoCreateOrUpdateInput input)
        {
            var villageInfo = ObjectMapper.Map<VillageInfo>(input);
            await _villageInfoRepository.InsertAsync(villageInfo);
        }
        
        private IQueryable<VillageInfo> GetFilteredQuery(GetVillageInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_villageInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
    }
}