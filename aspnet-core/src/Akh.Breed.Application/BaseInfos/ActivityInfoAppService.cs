using System.Collections.Generic;
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
    public class ActivityInfoAppService :  BreedAppServiceBase, IActivityInfoAppService
    {
        private readonly IRepository<ActivityInfo> _activityInfoRepository;

        public ActivityInfoAppService(IRepository<ActivityInfo> activityInfoRepository)
        {
            _activityInfoRepository = activityInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ActivityInfo)]
        public async Task<PagedResultDto<ActivityInfoListDto>> GetActivityInfo(GetActivityInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var activityInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var activityInfosListDto = ObjectMapper.Map<List<ActivityInfoListDto>>(activityInfos);
            return new PagedResultDto<ActivityInfoListDto>(
                userCount,
                activityInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ActivityInfo_Create, AppPermissions.Pages_BaseInfo_ActivityInfo_Edit)]
        public async Task<ActivityInfoCreateOrUpdateInput> GetActivityInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new ActivityInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var activityInfo = await _activityInfoRepository.GetAsync(input.Id.Value);
                if (activityInfo != null)
                    ObjectMapper.Map<ActivityInfo,ActivityInfoCreateOrUpdateInput>(activityInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ActivityInfo_Create, AppPermissions.Pages_BaseInfo_ActivityInfo_Edit)]
        public async Task CreateOrUpdateActivityInfo(ActivityInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateActivityInfoAsync(input);
            }
            else
            {
                await CreateActivityInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ActivityInfo_Delete)]
        public async Task DeleteActivityInfo(EntityDto input)
        {
            try
            {
                await _activityInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ActivityInfo_Edit)]
        private async Task UpdateActivityInfoAsync(ActivityInfoCreateOrUpdateInput input)
        {
            var activityInfo = ObjectMapper.Map<ActivityInfo>(input);
            await _activityInfoRepository.UpdateAsync(activityInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_ActivityInfo_Create)]
        private async Task CreateActivityInfoAsync(ActivityInfoCreateOrUpdateInput input)
        {
            var activityInfo = ObjectMapper.Map<ActivityInfo>(input);
            await _activityInfoRepository.InsertAsync(activityInfo);
        }
        
        private IQueryable<ActivityInfo> GetFilteredQuery(GetActivityInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_activityInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(ActivityInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _activityInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _activityInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}