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
    public class AnomalyInfoAppService :  BreedAppServiceBase, IAnomalyInfoAppService
    {
        private readonly IRepository<AnomalyInfo> _anomalyInfoRepository;

        public AnomalyInfoAppService(IRepository<AnomalyInfo> anomalyInfoRepository)
        {
            _anomalyInfoRepository = anomalyInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_AnomalyInfo)]
        public async Task<PagedResultDto<AnomalyInfoListDto>> GetAnomalyInfo(GetAnomalyInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var anomalyInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var anomalyInfosListDto = ObjectMapper.Map<List<AnomalyInfoListDto>>(anomalyInfos);
            return new PagedResultDto<AnomalyInfoListDto>(
                userCount,
                anomalyInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_AnomalyInfo_Create, AppPermissions.Pages_Inseminating_AnomalyInfo_Edit)]
        public async Task<AnomalyInfoCreateOrUpdateInput> GetAnomalyInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new AnomalyInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var anomalyInfo = await _anomalyInfoRepository.GetAsync(input.Id.Value);
                if (anomalyInfo != null)
                    ObjectMapper.Map<AnomalyInfo,AnomalyInfoCreateOrUpdateInput>(anomalyInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_AnomalyInfo_Create, AppPermissions.Pages_Inseminating_AnomalyInfo_Edit)]
        public async Task CreateOrUpdateAnomalyInfo(AnomalyInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateAnomalyInfoAsync(input);
            }
            else
            {
                await CreateAnomalyInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_AnomalyInfo_Delete)]
        public async Task DeleteAnomalyInfo(EntityDto input)
        {
            try
            {
                await _anomalyInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_AnomalyInfo_Edit)]
        private async Task UpdateAnomalyInfoAsync(AnomalyInfoCreateOrUpdateInput input)
        {
            var anomalyInfo = ObjectMapper.Map<AnomalyInfo>(input);
            await _anomalyInfoRepository.UpdateAsync(anomalyInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_AnomalyInfo_Create)]
        private async Task CreateAnomalyInfoAsync(AnomalyInfoCreateOrUpdateInput input)
        {
            var anomalyInfo = ObjectMapper.Map<AnomalyInfo>(input);
            await _anomalyInfoRepository.InsertAsync(anomalyInfo);
        }
        
        private IQueryable<AnomalyInfo> GetFilteredQuery(GetAnomalyInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_anomalyInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(AnomalyInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _anomalyInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _anomalyInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}