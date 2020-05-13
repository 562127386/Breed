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
    public class UnionInfoAppService :  BreedAppServiceBase, IUnionInfoAppService
    {
        private readonly IRepository<UnionInfo> _unionInfoRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;

        public UnionInfoAppService(IRepository<UnionInfo> unionInfoRepository, IRepository<StateInfo> stateInfoRepository)
        {
            _unionInfoRepository = unionInfoRepository;
            _stateInfoRepository = stateInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_UnionInfo)]
        public async Task<PagedResultDto<UnionInfoListDto>> GetUnionInfo(GetUnionInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var unionInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var unionInfosListDto = ObjectMapper.Map<List<UnionInfoListDto>>(unionInfos);
            return new PagedResultDto<UnionInfoListDto>(
                userCount,
                unionInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_UnionInfo_Create, AppPermissions.Pages_BaseInfo_UnionInfo_Edit)]
        public async Task<UnionInfoGetForEditOutput> GetUnionInfoForEdit(NullableIdDto<int> input)
        {
            UnionInfo unionInfo = null;
            if (input.Id.HasValue)
            {
                unionInfo = await _unionInfoRepository.GetAsync(input.Id.Value);
            }
            //Getting all available roles
            var output = new UnionInfoGetForEditOutput();
            
            output.UnionInfo = unionInfo != null
                ? ObjectMapper.Map<UnionInfoCreateOrUpdateInput>(unionInfo)
                : new UnionInfoCreateOrUpdateInput();
            
            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name)
                    {IsSelected = output.UnionInfo.StateInfoId.Equals(c.Id)})
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_UnionInfo_Create, AppPermissions.Pages_BaseInfo_UnionInfo_Edit)]
        public async Task CreateOrUpdateUnionInfo(UnionInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateUnionInfoAsync(input);
            }
            else
            {
                await CreateUnionInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_UnionInfo_Delete)]
        public async Task DeleteUnionInfo(EntityDto input)
        {
            try
            {
                await _unionInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_UnionInfo_Edit)]
        private async Task UpdateUnionInfoAsync(UnionInfoCreateOrUpdateInput input)
        {
            var unionInfo = ObjectMapper.Map<UnionInfo>(input);
            await _unionInfoRepository.UpdateAsync(unionInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_UnionInfo_Create)]
        private async Task CreateUnionInfoAsync(UnionInfoCreateOrUpdateInput input)
        {
            var unionInfo = ObjectMapper.Map<UnionInfo>(input);
            await _unionInfoRepository.InsertAsync(unionInfo);
        }
        
        private IQueryable<UnionInfo> GetFilteredQuery(GetUnionInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_unionInfoRepository.GetAllIncluding(p => p.StateInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        public List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input)
        {
            var query = _unionInfoRepository.GetAll();
            if (input.Id.HasValue)
            {
                query = query.Where(x => x.StateInfoId == input.Id);
            }
            
            return query.Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
        }
        private async Task CheckValidation(UnionInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _unionInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _unionInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}