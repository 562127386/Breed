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
    public class BirthTypeInfoAppService :  BreedAppServiceBase, IBirthTypeInfoAppService
    {
        private readonly IRepository<BirthTypeInfo> _birthTypeInfoRepository;

        public BirthTypeInfoAppService(IRepository<BirthTypeInfo> birthTypeInfoRepository)
        {
            _birthTypeInfoRepository = birthTypeInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_BirthTypeInfo)]
        public async Task<PagedResultDto<BirthTypeInfoListDto>> GetBirthTypeInfo(GetBirthTypeInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var birthTypeInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var birthTypeInfosListDto = ObjectMapper.Map<List<BirthTypeInfoListDto>>(birthTypeInfos);
            return new PagedResultDto<BirthTypeInfoListDto>(
                userCount,
                birthTypeInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BirthTypeInfo_Create, AppPermissions.Pages_Inseminating_BirthTypeInfo_Edit)]
        public async Task<BirthTypeInfoCreateOrUpdateInput> GetBirthTypeInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new BirthTypeInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var birthTypeInfo = await _birthTypeInfoRepository.GetAsync(input.Id.Value);
                if (birthTypeInfo != null)
                    ObjectMapper.Map<BirthTypeInfo,BirthTypeInfoCreateOrUpdateInput>(birthTypeInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BirthTypeInfo_Create, AppPermissions.Pages_Inseminating_BirthTypeInfo_Edit)]
        public async Task CreateOrUpdateBirthTypeInfo(BirthTypeInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateBirthTypeInfoAsync(input);
            }
            else
            {
                await CreateBirthTypeInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BirthTypeInfo_Delete)]
        public async Task DeleteBirthTypeInfo(EntityDto input)
        {
            try
            {
                await _birthTypeInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_BirthTypeInfo_Edit)]
        private async Task UpdateBirthTypeInfoAsync(BirthTypeInfoCreateOrUpdateInput input)
        {
            var birthTypeInfo = ObjectMapper.Map<BirthTypeInfo>(input);
            await _birthTypeInfoRepository.UpdateAsync(birthTypeInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_BirthTypeInfo_Create)]
        private async Task CreateBirthTypeInfoAsync(BirthTypeInfoCreateOrUpdateInput input)
        {
            var birthTypeInfo = ObjectMapper.Map<BirthTypeInfo>(input);
            await _birthTypeInfoRepository.InsertAsync(birthTypeInfo);
        }
        
        private IQueryable<BirthTypeInfo> GetFilteredQuery(GetBirthTypeInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_birthTypeInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(BirthTypeInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _birthTypeInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _birthTypeInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}