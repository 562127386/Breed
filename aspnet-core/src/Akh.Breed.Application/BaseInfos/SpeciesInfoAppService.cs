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
    public class SpeciesInfoAppService :  BreedAppServiceBase, ISpeciesInfoAppService
    {
        public const string CodePrefixStr = "364-0-52-"; 

        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;

        public SpeciesInfoAppService(IRepository<SpeciesInfo> speciesInfoRepository)
        {
            _speciesInfoRepository = speciesInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SpeciesInfo)]
        public async Task<PagedResultDto<SpeciesInfoListDto>> GetSpeciesInfo(GetSpeciesInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var speciesInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var speciesInfosListDto = ObjectMapper.Map<List<SpeciesInfoListDto>>(speciesInfos);
            return new PagedResultDto<SpeciesInfoListDto>(
                userCount,
                speciesInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SpeciesInfo_Create, AppPermissions.Pages_BaseInfo_SpeciesInfo_Edit)]
        public async Task<SpeciesInfoCreateOrUpdateInput> GetSpeciesInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new SpeciesInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var speciesInfo = await _speciesInfoRepository.GetAsync(input.Id.Value);
                if (speciesInfo != null)
                    ObjectMapper.Map<SpeciesInfo,SpeciesInfoCreateOrUpdateInput>(speciesInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SpeciesInfo_Create, AppPermissions.Pages_BaseInfo_SpeciesInfo_Edit)]
        public async Task CreateOrUpdateSpeciesInfo(SpeciesInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateSpeciesInfoAsync(input);
            }
            else
            {
                await CreateSpeciesInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SpeciesInfo_Delete)]
        public async Task DeleteSpeciesInfo(EntityDto input)
        {
            try
            {
                await _speciesInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        public async Task<string> GetCodeRange(EntityDto input)
        {
            var species = await _speciesInfoRepository.GetAsync(input.Id);
            return species != null ? species.ToCodeStr : "0";
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SpeciesInfo_Edit)]
        private async Task UpdateSpeciesInfoAsync(SpeciesInfoCreateOrUpdateInput input)
        {
            var speciesInfo = ObjectMapper.Map<SpeciesInfo>(input);
            await _speciesInfoRepository.UpdateAsync(speciesInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_SpeciesInfo_Create)]
        private async Task CreateSpeciesInfoAsync(SpeciesInfoCreateOrUpdateInput input)
        {
            var speciesInfo = ObjectMapper.Map<SpeciesInfo>(input);
            await _speciesInfoRepository.InsertAsync(speciesInfo);
        }
        
        private IQueryable<SpeciesInfo> GetFilteredQuery(GetSpeciesInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_speciesInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(SpeciesInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _speciesInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _speciesInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}