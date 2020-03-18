using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class UnionInfoAppService :  BreedAppServiceBase, IUnionInfoAppService
    {
        private readonly IRepository<UnionInfo> _unionInfoRepository;

        public UnionInfoAppService(IRepository<UnionInfo> unionInfoRepository)
        {
            _unionInfoRepository = unionInfoRepository;
        }

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
        
        public async Task<UnionInfoCreateOrUpdateInput> GetUnionInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new UnionInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var unionInfo = await _unionInfoRepository.GetAsync(input.Id.Value);
                if (unionInfo != null)
                    ObjectMapper.Map<UnionInfo,UnionInfoCreateOrUpdateInput>(unionInfo,output);
            }

            return output;
        }
        
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
        
        public async Task DeleteUnionInfo(EntityDto input)
        {
            await _unionInfoRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateUnionInfoAsync(UnionInfoCreateOrUpdateInput input)
        {
            var unionInfo = ObjectMapper.Map<UnionInfo>(input);
            await _unionInfoRepository.UpdateAsync(unionInfo);
        }
        
        private async Task CreateUnionInfoAsync(UnionInfoCreateOrUpdateInput input)
        {
            var unionInfo = ObjectMapper.Map<UnionInfo>(input);
            await _unionInfoRepository.InsertAsync(unionInfo);
        }
        
        private IQueryable<UnionInfo> GetFilteredQuery(GetUnionInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_unionInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
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