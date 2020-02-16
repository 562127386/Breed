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
    public class RegionInfoAppService :  BreedAppServiceBase, IRegionInfoAppService
    {
        private readonly IRepository<RegionInfo> _regionInfoRepository;

        public RegionInfoAppService(IRepository<RegionInfo> regionInfoRepository)
        {
            _regionInfoRepository = regionInfoRepository;
        }

        public async Task<PagedResultDto<RegionInfoListDto>> GetRegionInfo(GetRegionInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var regionInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var regionInfosListDto = ObjectMapper.Map<List<RegionInfoListDto>>(regionInfos);
            return new PagedResultDto<RegionInfoListDto>(
                userCount,
                regionInfosListDto
            );
        }
        
        public async Task<RegionInfoCreateOrUpdateInput> GetRegionInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new RegionInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var regionInfo = await _regionInfoRepository.GetAsync(input.Id.Value);
                if (regionInfo != null)
                    ObjectMapper.Map<RegionInfo,RegionInfoCreateOrUpdateInput>(regionInfo,output);
            }

            return output;
        }
        
        public async Task CreateOrUpdateRegionInfo(RegionInfoCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateRegionInfoAsync(input);
            }
            else
            {
                await CreateRegionInfoAsync(input);
            }
        }
        
        public async Task DeleteRegionInfo(EntityDto input)
        {
            await _regionInfoRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateRegionInfoAsync(RegionInfoCreateOrUpdateInput input)
        {
            var regionInfo = ObjectMapper.Map<RegionInfo>(input);
            await _regionInfoRepository.UpdateAsync(regionInfo);
        }
        
        private async Task CreateRegionInfoAsync(RegionInfoCreateOrUpdateInput input)
        {
            var regionInfo = ObjectMapper.Map<RegionInfo>(input);
            await _regionInfoRepository.InsertAsync(regionInfo);
        }
        
        private IQueryable<RegionInfo> GetFilteredQuery(GetRegionInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_regionInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
    }
}