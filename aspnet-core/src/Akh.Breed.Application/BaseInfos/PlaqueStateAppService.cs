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
    public class PlaqueStateAppService :  BreedAppServiceBase, IPlaqueStateAppService
    {
        private readonly IRepository<PlaqueState> _plaqueStateRepository;

        public PlaqueStateAppService(IRepository<PlaqueState> plaqueStateRepository)
        {
            _plaqueStateRepository = plaqueStateRepository;
        }

        public async Task<PagedResultDto<PlaqueStateListDto>> GetPlaqueState(GetPlaqueStateInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var plaqueStates = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var plaqueStatesListDto = ObjectMapper.Map<List<PlaqueStateListDto>>(plaqueStates);
            return new PagedResultDto<PlaqueStateListDto>(
                userCount,
                plaqueStatesListDto
            );
        }
        
        public async Task<PlaqueStateCreateOrUpdateInput> GetPlaqueStateForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new PlaqueStateCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var plaqueState = await _plaqueStateRepository.GetAsync(input.Id.Value);
                if (plaqueState != null)
                    ObjectMapper.Map<PlaqueState,PlaqueStateCreateOrUpdateInput>(plaqueState,output);
            }

            return output;
        }
        
        public async Task CreateOrUpdatePlaqueState(PlaqueStateCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdatePlaqueStateAsync(input);
            }
            else
            {
                await CreatePlaqueStateAsync(input);
            }
        }
        
        public async Task DeletePlaqueState(EntityDto input)
        {
            try
            {
                await _plaqueStateRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        private async Task UpdatePlaqueStateAsync(PlaqueStateCreateOrUpdateInput input)
        {
            var plaqueState = ObjectMapper.Map<PlaqueState>(input);
            await _plaqueStateRepository.UpdateAsync(plaqueState);
        }
        
        private async Task CreatePlaqueStateAsync(PlaqueStateCreateOrUpdateInput input)
        {
            var plaqueState = ObjectMapper.Map<PlaqueState>(input);
            await _plaqueStateRepository.InsertAsync(plaqueState);
        }
        
        private IQueryable<PlaqueState> GetFilteredQuery(GetPlaqueStateInput input)
        {
            var query = QueryableExtensions.WhereIf(_plaqueStateRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(PlaqueStateCreateOrUpdateInput input)
        {
            var existingObj = (await _plaqueStateRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _plaqueStateRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}