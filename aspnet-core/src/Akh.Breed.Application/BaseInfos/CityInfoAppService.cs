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
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class CityInfoAppService :  BreedAppServiceBase, ICityInfoAppService
    {
        private readonly IRepository<CityInfo> _cityInfoRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;

        public CityInfoAppService(IRepository<CityInfo> cityInfoRepository, IRepository<StateInfo> stateInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
            _stateInfoRepository = stateInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_CityInfo)]
        public async Task<PagedResultDto<CityInfoListDto>> GetCityInfo(GetCityInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var cityInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var cityInfosListDto = ObjectMapper.Map<List<CityInfoListDto>>(cityInfos);
            return new PagedResultDto<CityInfoListDto>(
                userCount,
                cityInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_CityInfo_Create, AppPermissions.Pages_BaseInfo_CityInfo_Edit)]
        public async Task<CityInfoGetForEditOutput> GetCityInfoForEdit(NullableIdDto<int> input)
        {
            CityInfo cityInfo = null;
            if (input.Id.HasValue)
            {
                cityInfo = await _cityInfoRepository.GetAsync(input.Id.Value);
            }

            //Getting all available roles
            var output = new CityInfoGetForEditOutput();
            
            //cityInfo
            output.CityInfo = cityInfo != null
                ? ObjectMapper.Map<CityInfoCreateOrUpdateInput>(cityInfo)
                : new CityInfoCreateOrUpdateInput();
            
            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name)
                    {IsSelected = output.CityInfo.StateInfoId.Equals(c.Id)})
                .ToList();
                
            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_CityInfo_Create, AppPermissions.Pages_BaseInfo_CityInfo_Edit)]
        public async Task CreateOrUpdateCityInfo(CityInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateCityInfoAsync(input);
            }
            else
            {
                await CreateCityInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_CityInfo_Delete)]
        public async Task DeleteCityInfo(EntityDto input)
        {
            try
            {
                await _cityInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_CityInfo_Edit)]
        private async Task UpdateCityInfoAsync(CityInfoCreateOrUpdateInput input)
        {
            var cityInfo = ObjectMapper.Map<CityInfo>(input);
            await _cityInfoRepository.UpdateAsync(cityInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_CityInfo_Create)]
        private async Task CreateCityInfoAsync(CityInfoCreateOrUpdateInput input)
        {
            var cityInfo = ObjectMapper.Map<CityInfo>(input);
            await _cityInfoRepository.InsertAsync(cityInfo);
        }
        
        private IQueryable<CityInfo> GetFilteredQuery(GetCityInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_cityInfoRepository.GetAllIncluding(p => p.StateInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.StateInfo.Name.Contains(input.Filter) ||
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }

        public List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input)
        {
            var query = _cityInfoRepository.GetAll();
            if (input.Id.HasValue)
            {
                query = query.Where(x => x.StateInfoId == input.Id);
            }
            
            return query.Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
        }
        
        public string GetCode(NullableIdDto<int> input)
        {
            string res = "";
            if (input.Id.HasValue)
            {
                var cityInfo = _cityInfoRepository
                    .GetAll()
                    .Include(x => x.StateInfo)
                    .FirstOrDefault(x => x.Id == input.Id);
                res = cityInfo?.StateInfo.Code
                      + cityInfo?.Code;
            }
            
            return res;
        }
        
        private async Task CheckValidation(CityInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _cityInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code && l.StateInfoId == input.StateInfoId));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _cityInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name && l.StateInfoId == input.StateInfoId));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}