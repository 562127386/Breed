using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class RegionInfoAppService :  BreedAppServiceBase, IRegionInfoAppService
    {
        private readonly IRepository<RegionInfo> _regionInfoRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<CityInfo> _cityInfoRepository;


        public RegionInfoAppService(IRepository<RegionInfo> regionInfoRepository, IRepository<StateInfo> stateInfoRepository, IRepository<CityInfo> cityInfoRepository)
        {
            _regionInfoRepository = regionInfoRepository;
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
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
        
        public async Task<RegionInfoGetForEditOutput> GetRegionInfoForEdit(NullableIdDto<int> input)
        {
            RegionInfo regionInfo = null;
            if (input.Id.HasValue)
            {
                regionInfo = await _regionInfoRepository
                    .GetAll()
                    .Include(x => x.CityInfo)
                    .Where(x => x.Id == input.Id.Value)
                    .FirstOrDefaultAsync();
            }

            //Getting all available roles
            var output = new RegionInfoGetForEditOutput();
            
            //regionInfo
            output.RegionInfo = regionInfo != null
                ? ObjectMapper.Map<RegionInfoCreateOrUpdateInput>(regionInfo)
                : new RegionInfoCreateOrUpdateInput();
            
            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            if (output.RegionInfo.StateInfoId.HasValue)
            {
                output.CityInfos = _cityInfoRepository.GetAll()
                    .Where(x => x.StateInfoId == output.RegionInfo.StateInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }

            return output;
        }
        
        public async Task CreateOrUpdateRegionInfo(RegionInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
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
            var query = QueryableExtensions.WhereIf(
                _regionInfoRepository.GetAll()
                    .Include(p => p.CityInfo)
                    .ThenInclude(p => p.StateInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.CityInfo.StateInfo.Name.Contains(input.Filter) ||
                    u.CityInfo.Name.Contains(input.Filter) ||
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        public List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input)
        {
            var query = _regionInfoRepository.GetAll();
            if (input.Id.HasValue)
            {
                query = query.Where(x => x.CityInfoId == input.Id);
            }
            
            return query.Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
        }
        
        private async Task CheckValidation(RegionInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _regionInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code && l.CityInfoId == input.CityInfoId));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _regionInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name && l.CityInfoId == input.CityInfoId));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}