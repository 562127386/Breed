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
    public class BreedInfoAppService :  BreedAppServiceBase, IBreedInfoAppService
    {
        private readonly IRepository<BreedInfo> _breedInfoRepository;

        public BreedInfoAppService(IRepository<BreedInfo> breedInfoRepository)
        {
            _breedInfoRepository = breedInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_BreedInfo)]
        public async Task<PagedResultDto<BreedInfoListDto>> GetBreedInfo(GetBreedInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var breedInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var breedInfosListDto = ObjectMapper.Map<List<BreedInfoListDto>>(breedInfos);
            return new PagedResultDto<BreedInfoListDto>(
                userCount,
                breedInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BreedInfo_Create, AppPermissions.Pages_Inseminating_BreedInfo_Edit)]
        public async Task<BreedInfoCreateOrUpdateInput> GetBreedInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new BreedInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var breedInfo = await _breedInfoRepository.GetAsync(input.Id.Value);
                if (breedInfo != null)
                    ObjectMapper.Map<BreedInfo,BreedInfoCreateOrUpdateInput>(breedInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BreedInfo_Create, AppPermissions.Pages_Inseminating_BreedInfo_Edit)]
        public async Task CreateOrUpdateBreedInfo(BreedInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateBreedInfoAsync(input);
            }
            else
            {
                await CreateBreedInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BreedInfo_Delete)]
        public async Task DeleteBreedInfo(EntityDto input)
        {
            try
            {
                await _breedInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_BreedInfo_Edit)]
        private async Task UpdateBreedInfoAsync(BreedInfoCreateOrUpdateInput input)
        {
            var breedInfo = ObjectMapper.Map<BreedInfo>(input);
            await _breedInfoRepository.UpdateAsync(breedInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BreedInfo_Create)]
        private async Task CreateBreedInfoAsync(BreedInfoCreateOrUpdateInput input)
        {
            var breedInfo = ObjectMapper.Map<BreedInfo>(input);
            await _breedInfoRepository.InsertAsync(breedInfo);
        }
        
        private IQueryable<BreedInfo> GetFilteredQuery(GetBreedInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_breedInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(BreedInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _breedInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _breedInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}