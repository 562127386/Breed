﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class StateInfoAppService :  BreedAppServiceBase, IStateInfoAppService
    {
        private readonly IRepository<StateInfo> _stateInfoRepository;

        public StateInfoAppService(IRepository<StateInfo> stateInfoRepository)
        {
            _stateInfoRepository = stateInfoRepository;
        }

        public async Task<PagedResultDto<StateInfoListDto>> GetStateInfo(GetStateInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var stateInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var stateInfosListDto = ObjectMapper.Map<List<StateInfoListDto>>(stateInfos);
            return new PagedResultDto<StateInfoListDto>(
                userCount,
                stateInfosListDto
            );
        }
        
        public async Task<StateInfoCreateOrUpdateInput> GetStateInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new StateInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var stateInfo = await _stateInfoRepository.GetAsync(input.Id.Value);
                if (stateInfo != null)
                    ObjectMapper.Map<StateInfo,StateInfoCreateOrUpdateInput>(stateInfo,output);
            }

            return output;
        }
        
        public async Task CreateOrUpdateStateInfo(StateInfoCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateStateInfoAsync(input);
            }
            else
            {
                await CreateStateInfoAsync(input);
            }
        }
        
        public async Task DeleteStateInfo(EntityDto input)
        {
            await _stateInfoRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateStateInfoAsync(StateInfoCreateOrUpdateInput input)
        {
            var stateInfo = ObjectMapper.Map<StateInfo>(input);
            await _stateInfoRepository.UpdateAsync(stateInfo);
        }
        
        private async Task CreateStateInfoAsync(StateInfoCreateOrUpdateInput input)
        {
            var stateInfo = ObjectMapper.Map<StateInfo>(input);
            await _stateInfoRepository.InsertAsync(stateInfo);
        }
        
        private IQueryable<StateInfo> GetFilteredQuery(GetStateInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_stateInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
    }
}