using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.BaseInfo;
using Akh.Breed.Inseminating;
using Akh.Breed.Inseminating.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Inseminating
{
    public class SpotConnectorInfoAppService :  BreedAppServiceBase, ISpotConnectorInfoAppService
    {
        private readonly IRepository<SpotConnectorInfo> _spotConnectorInfoRepository;

        public SpotConnectorInfoAppService(IRepository<SpotConnectorInfo> spotConnectorInfoRepository)
        {
            _spotConnectorInfoRepository = spotConnectorInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_SpotConnectorInfo)]
        public async Task<PagedResultDto<SpotConnectorInfoListDto>> GetSpotConnectorInfo(GetSpotConnectorInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var spotConnectorInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var spotConnectorInfosListDto = ObjectMapper.Map<List<SpotConnectorInfoListDto>>(spotConnectorInfos);
            return new PagedResultDto<SpotConnectorInfoListDto>(
                userCount,
                spotConnectorInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_SpotConnectorInfo_Create, AppPermissions.Pages_Inseminating_SpotConnectorInfo_Edit)]
        public async Task<SpotConnectorInfoCreateOrUpdateInput> GetSpotConnectorInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new SpotConnectorInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var spotConnectorInfo = await _spotConnectorInfoRepository.GetAsync(input.Id.Value);
                if (spotConnectorInfo != null)
                    ObjectMapper.Map<SpotConnectorInfo,SpotConnectorInfoCreateOrUpdateInput>(spotConnectorInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_SpotConnectorInfo_Create, AppPermissions.Pages_Inseminating_SpotConnectorInfo_Edit)]
        public async Task CreateOrUpdateSpotConnectorInfo(SpotConnectorInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateSpotConnectorInfoAsync(input);
            }
            else
            {
                await CreateSpotConnectorInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_SpotConnectorInfo_Delete)]
        public async Task DeleteSpotConnectorInfo(EntityDto input)
        {
            try
            {
                await _spotConnectorInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_SpotConnectorInfo_Edit)]
        private async Task UpdateSpotConnectorInfoAsync(SpotConnectorInfoCreateOrUpdateInput input)
        {
            var spotConnectorInfo = ObjectMapper.Map<SpotConnectorInfo>(input);
            await _spotConnectorInfoRepository.UpdateAsync(spotConnectorInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_SpotConnectorInfo_Create)]
        private async Task CreateSpotConnectorInfoAsync(SpotConnectorInfoCreateOrUpdateInput input)
        {
            var spotConnectorInfo = ObjectMapper.Map<SpotConnectorInfo>(input);
            await _spotConnectorInfoRepository.InsertAsync(spotConnectorInfo);
        }
        
        private IQueryable<SpotConnectorInfo> GetFilteredQuery(GetSpotConnectorInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_spotConnectorInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(SpotConnectorInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _spotConnectorInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _spotConnectorInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}