﻿using System.Collections.Generic;
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
        private readonly IRepository<RegionInfo> _regionInfoRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<CityInfo> _cityInfoRepository;

        public VillageInfoAppService(IRepository<VillageInfo> villageInfoRepository, IRepository<RegionInfo> regionInfoRepository, IRepository<StateInfo> stateInfoRepository, IRepository<CityInfo> cityInfoRepository)
        {
            _villageInfoRepository = villageInfoRepository;
            _regionInfoRepository = regionInfoRepository;
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
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
        
        public async Task<VillageInfoGetForEditOutput> GetVillageInfoForEdit(NullableIdDto<int> input)
        {
            VillageInfo villageInfo = null;
            if (input.Id.HasValue)
            {
                villageInfo = await _villageInfoRepository
                    .GetAll()
                    .Include(x => x.RegionInfo)
                    .ThenInclude(x => x.CityInfo)
                    .Where(x => x.Id == input.Id.Value)
                    .FirstOrDefaultAsync();
            }
            //Getting all available roles
            var output = new VillageInfoGetForEditOutput();
            
            //villageInfo
            output.VillageInfo = villageInfo != null
                ? ObjectMapper.Map<VillageInfoCreateOrUpdateInput>(villageInfo)
                : new VillageInfoCreateOrUpdateInput();
            
            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            if (output.VillageInfo.StateInfoId.HasValue)
            {
                output.CityInfos = _cityInfoRepository.GetAll()
                    .Where(x => x.StateInfoId == output.VillageInfo.StateInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            if (output.VillageInfo.CityInfoId.HasValue)
            {
                output.RegionInfos = _regionInfoRepository.GetAll()
                    .Where(x => x.CityInfoId == output.VillageInfo.CityInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
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
            var query = QueryableExtensions.WhereIf(
                _villageInfoRepository.GetAll()
                .Include(p => p.RegionInfo)
                .ThenInclude(p => p.CityInfo)
                .ThenInclude(p => p.StateInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.RegionInfo.CityInfo.StateInfo.Name.Contains(input.Filter) ||
                    u.RegionInfo.CityInfo.Name.Contains(input.Filter) ||
                    u.RegionInfo.Name.Contains(input.Filter) ||
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
    }
}