﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors;
using Akh.Breed.Herds.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Herds
{
    public class HerdAppService : BreedAppServiceBase, IHerdAppService
    {
        private readonly IRepository<Herd> _herdRepository;
        private readonly IRepository<HerdGeoLog> _herdGeoLogInfoRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<CityInfo> _cityInfoRepository;
        private readonly IRepository<RegionInfo> _regionInfoRepository;
        private readonly IRepository<VillageInfo> _villageInfoRepository;
        private readonly IRepository<UnionInfo> _unionInfoRepository;
        private readonly IRepository<ActivityInfo> _activityInfoRepository;
        private readonly IRepository<Contractor> _contractorRepository;
        
        public HerdAppService(IRepository<Herd> herdRepository, IRepository<StateInfo> stateInfoRepository, IRepository<CityInfo> cityInfoRepository, IRepository<RegionInfo> regionInfoRepository, IRepository<VillageInfo> villageInfoRepository, IRepository<UnionInfo> unionInfoRepository, IRepository<HerdGeoLog> herdGeoLogInfoRepository, IRepository<ActivityInfo> activityInfoRepository, IRepository<Contractor> contractorRepository)
        {
            _herdRepository = herdRepository;
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
            _regionInfoRepository = regionInfoRepository;
            _villageInfoRepository = villageInfoRepository;
            _unionInfoRepository = unionInfoRepository;
            _herdGeoLogInfoRepository = herdGeoLogInfoRepository;
            _activityInfoRepository = activityInfoRepository;
            _contractorRepository = contractorRepository;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Herd)]
        public async Task<PagedResultDto<HerdListDto>> GetHerd(GetHerdInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var herds = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var herdsListDto = ObjectMapper.Map<List<HerdListDto>>(herds);
            return new PagedResultDto<HerdListDto>(
                userCount,
                herdsListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Herd_Create, AppPermissions.Pages_BaseIntro_Herd_Edit)]
        public async Task<GetHerdForEditOutput> GetHerdForEdit(NullableIdDto<int> input)
        {
            Herd herd = null;
            if (input.Id.HasValue)
            {
                herd = await _herdRepository.GetAll()
                    .Include(x => x.VillageInfo)
                    .Include(x => x.RegionInfo)
                    .Include(x => x.CityInfo)
                    .Include(x => x.StateInfo)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new GetHerdForEditOutput();
            
            //herd
            output.Herd = herd != null
                ? ObjectMapper.Map<HerdCreateOrUpdateInput>(herd)
                : new HerdCreateOrUpdateInput();
            
           //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            if (output.Herd.StateInfoId.HasValue)
            {
                output.CityInfos = _cityInfoRepository.GetAll()
                    .Where(x => x.StateInfoId == output.Herd.StateInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            if (output.Herd.CityInfoId.HasValue)
            {
                output.RegionInfos = _regionInfoRepository.GetAll()
                    .Where(x => x.CityInfoId == output.Herd.CityInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            if (output.Herd.RegionInfoId.HasValue)
            {
                output.VillageInfos = _villageInfoRepository.GetAll()
                    .Where(x => x.RegionInfoId == output.Herd.RegionInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            output.UnionInfos = _unionInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            output.ActivityInfos = _activityInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            if (output.Herd.CityInfoId.HasValue)
            {
                output.Contractors = _contractorRepository
                    .GetAllList()
                    .Where(x => x.CityInfoId == output.Herd.CityInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.FirmName + " (" +c.Name+","+c.Family+")" ))
                    .ToList();
            }

            
            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Herd_Create, AppPermissions.Pages_BaseIntro_Herd_Edit)]
        public async Task CreateOrUpdateHerd(HerdCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateHerdAsync(input);
            }
            else
            {
                await CreateHerdAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Herd_Delete)]
        public async Task DeleteHerd(EntityDto input)
        {
            try
            {
                await _herdRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Herd_Edit)]
        private async Task UpdateHerdAsync(HerdCreateOrUpdateInput input)
        {
            var herd = ObjectMapper.Map<Herd>(input);
            await _herdRepository.UpdateAsync(herd);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Herd_Create)]
        private async Task CreateHerdAsync(HerdCreateOrUpdateInput input)
        {
            var herd = ObjectMapper.Map<Herd>(input);
            await _herdRepository.InsertAsync(herd);
            
            await CurrentUnitOfWork.SaveChangesAsync();
            
            var herdGeoLog = new HerdGeoLog
            {
                HerdId = herd.Id,
                Latitude = herd.Latitude,
                Longitude = herd.Longitude,
                CreationTime = herd.CreationTime
            };
            await _herdGeoLogInfoRepository.InsertAsync(herdGeoLog);
        }
        
        public List<ComboboxItemDto> GetContractorForCombo(NullableIdDto<int> input)
        {
            var query = _contractorRepository
                .GetAll();
            if (input.Id.HasValue)
            {
                query = query.Where(x => x.CityInfoId == input.Id);
            }
            
            return query.Select(c => new ComboboxItemDto(c.Id.ToString(), c.FirmName + " (" +c.Name+","+c.Family+")" ))
                .ToList();
        }
        
        private IQueryable<Herd> GetFilteredQuery(GetHerdInput input)
        {
            var tUser = UserManager.GetUserById(AbpSession.GetUserId());
            var isOfficer = UserManager.IsInRoleAsync(tUser,StaticRoleNames.Host.Officer).Result;
            
            var query = QueryableExtensions.WhereIf(
                _herdRepository.GetAll()
                .Include(x => x.Contractor)
                .Include(x => x.ActivityInfo)
                .Include(x => x.StateInfo)
                .Include(x => x.CityInfo)
                .Include(x => x.RegionInfo)
                .Include(x => x.VillageInfo)
                .Include(x => x.UnionInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Family.Contains(input.Filter) ||
                    u.NationalCode.Replace("-","").Contains(input.Filter) ||
                    u.Mobile.Replace("-","").Contains(input.Filter) ||
                    u.PostalCode.Replace("-","").Contains(input.Filter) ||
                    u.HerdName.Contains(input.Filter) ||
                    u.EpidemiologicCode.Contains(input.Filter) ||
                    u.Contractor.Name.Contains(input.Filter) ||
                    u.Contractor.Family.Contains(input.Filter) ||
                    u.Contractor.FirmName.Contains(input.Filter) ||
                    u.StateInfo.Name.Contains(input.Filter) ||
                    u.CityInfo.Name.Contains(input.Filter) ||
                    u.RegionInfo.Name.Contains(input.Filter) ||
                    u.VillageInfo.Name.Contains(input.Filter));

            if (isOfficer)
            {
                query = query.Where(x => x.CreatorUserId == AbpSession.UserId);
            }

            return query;
        }        
   }
}