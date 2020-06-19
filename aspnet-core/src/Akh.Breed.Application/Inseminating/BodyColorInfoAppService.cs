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
    public class BodyColorInfoAppService :  BreedAppServiceBase, IBodyColorInfoAppService
    {
        private readonly IRepository<BodyColorInfo> _bodyColorInfoRepository;

        public BodyColorInfoAppService(IRepository<BodyColorInfo> bodyColorInfoRepository)
        {
            _bodyColorInfoRepository = bodyColorInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_BodyColorInfo)]
        public async Task<PagedResultDto<BodyColorInfoListDto>> GetBodyColorInfo(GetBodyColorInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var bodyColorInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var bodyColorInfosListDto = ObjectMapper.Map<List<BodyColorInfoListDto>>(bodyColorInfos);
            return new PagedResultDto<BodyColorInfoListDto>(
                userCount,
                bodyColorInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BodyColorInfo_Create, AppPermissions.Pages_Inseminating_BodyColorInfo_Edit)]
        public async Task<BodyColorInfoCreateOrUpdateInput> GetBodyColorInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new BodyColorInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var bodyColorInfo = await _bodyColorInfoRepository.GetAsync(input.Id.Value);
                if (bodyColorInfo != null)
                    ObjectMapper.Map<BodyColorInfo,BodyColorInfoCreateOrUpdateInput>(bodyColorInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BodyColorInfo_Create, AppPermissions.Pages_Inseminating_BodyColorInfo_Edit)]
        public async Task CreateOrUpdateBodyColorInfo(BodyColorInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateBodyColorInfoAsync(input);
            }
            else
            {
                await CreateBodyColorInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BodyColorInfo_Delete)]
        public async Task DeleteBodyColorInfo(EntityDto input)
        {
            try
            {
                await _bodyColorInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_BodyColorInfo_Edit)]
        private async Task UpdateBodyColorInfoAsync(BodyColorInfoCreateOrUpdateInput input)
        {
            var bodyColorInfo = ObjectMapper.Map<BodyColorInfo>(input);
            await _bodyColorInfoRepository.UpdateAsync(bodyColorInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BodyColorInfo_Create)]
        private async Task CreateBodyColorInfoAsync(BodyColorInfoCreateOrUpdateInput input)
        {
            var bodyColorInfo = ObjectMapper.Map<BodyColorInfo>(input);
            await _bodyColorInfoRepository.InsertAsync(bodyColorInfo);
        }
        
        private IQueryable<BodyColorInfo> GetFilteredQuery(GetBodyColorInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_bodyColorInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(BodyColorInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _bodyColorInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _bodyColorInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}