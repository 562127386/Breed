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
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class ProviderInfoAppService :  BreedAppServiceBase, IProviderInfoAppService
    {
        private readonly IRepository<ProviderInfo> _providerInfoRepository;

        public ProviderInfoAppService(IRepository<ProviderInfo> providerInfoRepository)
        {
            _providerInfoRepository = providerInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ProviderInfo)]
        public async Task<PagedResultDto<ProviderInfoListDto>> GetProviderInfo(GetProviderInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var providerInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var providerInfosListDto = ObjectMapper.Map<List<ProviderInfoListDto>>(providerInfos);
            return new PagedResultDto<ProviderInfoListDto>(
                userCount,
                providerInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ProviderInfo_Create, AppPermissions.Pages_BaseInfo_ProviderInfo_Edit)]
        public async Task<ProviderInfoCreateOrUpdateInput> GetProviderInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new ProviderInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var providerInfo = await _providerInfoRepository.GetAsync(input.Id.Value);
                if (providerInfo != null)
                    ObjectMapper.Map<ProviderInfo,ProviderInfoCreateOrUpdateInput>(providerInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ProviderInfo_Create, AppPermissions.Pages_BaseInfo_ProviderInfo_Edit)]
        public async Task CreateOrUpdateProviderInfo(ProviderInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateProviderInfoAsync(input);
            }
            else
            {
                await CreateProviderInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ProviderInfo_Delete)]
        public async Task DeleteProviderInfo(EntityDto input)
        {
            try
            {
                await _providerInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ProviderInfo_Edit)]
        private async Task UpdateProviderInfoAsync(ProviderInfoCreateOrUpdateInput input)
        {
            var providerInfo = ObjectMapper.Map<ProviderInfo>(input);
            await _providerInfoRepository.UpdateAsync(providerInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ProviderInfo_Create)]
        private async Task CreateProviderInfoAsync(ProviderInfoCreateOrUpdateInput input)
        {
            var providerInfo = ObjectMapper.Map<ProviderInfo>(input);
            await _providerInfoRepository.InsertAsync(providerInfo);
        }
        
        private IQueryable<ProviderInfo> GetFilteredQuery(GetProviderInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_providerInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(ProviderInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _providerInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _providerInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}