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
    public class SexInfoAppService :  BreedAppServiceBase, ISexInfoAppService
    {
        private readonly IRepository<SexInfo> _sexInfoRepository;

        public SexInfoAppService(IRepository<SexInfo> sexInfoRepository)
        {
            _sexInfoRepository = sexInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SexInfo)]
        public async Task<PagedResultDto<SexInfoListDto>> GetSexInfo(GetSexInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var sexInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var sexInfosListDto = ObjectMapper.Map<List<SexInfoListDto>>(sexInfos);
            return new PagedResultDto<SexInfoListDto>(
                userCount,
                sexInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SexInfo_Create, AppPermissions.Pages_BaseInfo_SexInfo_Edit)]
        public async Task<SexInfoCreateOrUpdateInput> GetSexInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new SexInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var sexInfo = await _sexInfoRepository.GetAsync(input.Id.Value);
                if (sexInfo != null)
                    ObjectMapper.Map<SexInfo,SexInfoCreateOrUpdateInput>(sexInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SexInfo_Create, AppPermissions.Pages_BaseInfo_SexInfo_Edit)]
        public async Task CreateOrUpdateSexInfo(SexInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateSexInfoAsync(input);
            }
            else
            {
                await CreateSexInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SexInfo_Delete)]
        public async Task DeleteSexInfo(EntityDto input)
        {
            try
            {
                await _sexInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SexInfo_Edit)]
        private async Task UpdateSexInfoAsync(SexInfoCreateOrUpdateInput input)
        {
            var sexInfo = ObjectMapper.Map<SexInfo>(input);
            await _sexInfoRepository.UpdateAsync(sexInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SexInfo_Create)]
        private async Task CreateSexInfoAsync(SexInfoCreateOrUpdateInput input)
        {
            var sexInfo = ObjectMapper.Map<SexInfo>(input);
            await _sexInfoRepository.InsertAsync(sexInfo);
        }
        
        private IQueryable<SexInfo> GetFilteredQuery(GetSexInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_sexInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(SexInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _sexInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _sexInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
        
    }
}