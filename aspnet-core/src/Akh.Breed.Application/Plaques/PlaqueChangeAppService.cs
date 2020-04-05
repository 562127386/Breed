using System;
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
using Akh.Breed.Officers;
using Akh.Breed.Plaques.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Plaques
{
    public class PlaqueChangeAppService :  BreedAppServiceBase, IPlaqueChangeAppService
    {
        private readonly IRepository<PlaqueChange> _plaqueChangeRepository;
        private readonly IRepository<PlaqueState> _plaqueStateRepository;
        private readonly IRepository<PlaqueStore> _plaqueStoreRepository;
        private readonly IRepository<PlaqueInfo, long> _plaqueInfoRepository;
        private readonly IRepository<Officer> _officerRepository;
        
        public PlaqueChangeAppService(IRepository<PlaqueChange> plaqueChangeRepository, IRepository<PlaqueState> plaqueStateRepository, IRepository<PlaqueStore> plaqueStoreRepository, IRepository<PlaqueInfo, long> plaqueInfoRepository, IRepository<Officer> officerRepository)
        {
            _plaqueChangeRepository = plaqueChangeRepository;
            _plaqueStateRepository = plaqueStateRepository;
            _plaqueStoreRepository = plaqueStoreRepository;
            _plaqueInfoRepository = plaqueInfoRepository;
            _officerRepository = officerRepository;
        }

        public async Task<PagedResultDto<PlaqueChangeListDto>> GetPlaqueChange(GetPlaqueChangeInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var plaqueChanges = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var plaqueChangesListDto = ObjectMapper.Map<List<PlaqueChangeListDto>>(plaqueChanges);
            return new PagedResultDto<PlaqueChangeListDto>(
                userCount,
                plaqueChangesListDto
            );
        }
        
        public async Task<PlaqueChangeGetForEditOutput> GetPlaqueChangeForEdit(NullableIdDto<int> input)
        {
            PlaqueChange plaqueChange = null;
            if (input.Id.HasValue)
            {
                plaqueChange = await _plaqueChangeRepository
                    .GetAll()
                    .Where(x => x.Id == input.Id.Value)
                    .FirstOrDefaultAsync();
            }
            //Getting all available roles
            var output = new PlaqueChangeGetForEditOutput();
            
            //plaqueChange
            output.PlaqueChange = plaqueChange != null
                ? ObjectMapper.Map<PlaqueChangeCreateOrUpdateInput>(plaqueChange)
                : new PlaqueChangeCreateOrUpdateInput();
            
            //speciesInfo
            output.PlaqueStates = _plaqueStateRepository
                .GetAllList()
                .Where(x => x.Id != 1)
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            return output;
        }
        
        public async Task CreateOrUpdatePlaqueChange(PlaqueChangeCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdatePlaqueChangeAsync(input);
            }
            else
            {
                await CreatePlaqueChangeAsync(input);
            }
        }
        
        public async Task DeletePlaqueChange(EntityDto input)
        {
            try
            {
                await _plaqueChangeRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        public Task CheckPlaqueCode(string input)
        {
            throw new NotImplementedException();
        }

        private async Task UpdatePlaqueChangeAsync(PlaqueChangeCreateOrUpdateInput input)
        {
            var plaqueChange = ObjectMapper.Map<PlaqueChange>(input);
            await _plaqueChangeRepository.UpdateAsync(plaqueChange);
        }
        
        private async Task CreatePlaqueChangeAsync(PlaqueChangeCreateOrUpdateInput input)
        {
            if (input.PlaqueId.HasValue)
            {
                var plaqueInfo = _plaqueInfoRepository.Get(input.PlaqueId.Value);
                plaqueInfo.StateId = input.NewStateId;
                await _plaqueInfoRepository.UpdateAsync(plaqueInfo);
            }
            else
            {
                var plaqueInfo = new PlaqueInfo { 
                    Code = Convert.ToInt64(input.PlaqueCode),
                    OfficerId = input.OfficerId,
                    StateId = input.NewStateId,
                    Latitude = "",
                    Longitude = ""
                };
                await _plaqueInfoRepository.InsertAsync(plaqueInfo);
                await CurrentUnitOfWork.SaveChangesAsync();
                input.PlaqueId = plaqueInfo.Id;
            }
            var plaqueChange = ObjectMapper.Map<PlaqueChange>(input);
            await _plaqueChangeRepository.InsertAsync(plaqueChange);
        }
        
        private IQueryable<PlaqueChange> GetFilteredQuery(GetPlaqueChangeInput input)
        {
            long tempSearch = Convert.ToInt64(input.Filter);
            var query = QueryableExtensions.WhereIf(
                _plaqueChangeRepository.GetAll()
                    .Include(x => x.Plaque)
                    .ThenInclude(x => x.Livestock)
                    .ThenInclude(x => x.Herd)
                    .Include(x => x.PreState)
                    .Include(x => x.NewState)
                    .Include(x => x.Officer),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Plaque.Code.ToString().Contains(input.Filter));

            return query;
        }
        
        public async Task<PlaqueChangeCreateOrUpdateInput> CheckValidation(PlaqueChangeCreateOrUpdateInput input)
        {
            var officer = _officerRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
            if (officer == null)
            {
                throw new UserFriendlyException(L("TheOfficerDoesNotExists"));
            }
            else
            {
                input.OfficerId = officer.Id;
            }
            
            long code = 364052000000000 + Convert.ToInt64(input.PlaqueCode);
            var plaqueStores = _plaqueStoreRepository
                .FirstOrDefault(x => x.FromCode <= code && x.ToCode >= code);
            if (plaqueStores == null)
            {
                throw new UserFriendlyException(L("ThePlaqueStoreDoesNotExists"));
            }
            
            var plaqueChanged = _plaqueChangeRepository.GetAll()
                .Include(x => x.Plaque)
                .FirstOrDefault(x => x.Plaque.Code == code);
            if (plaqueChanged != null)
            {
                throw new UserFriendlyException(L("ThisCodeChanged"));
            }

            var plaqueInfo = _plaqueInfoRepository.GetAll()
                .Include(x => x.State)
                .Include(x => x.Livestock)
                .ThenInclude(x => x.Herd)
                .FirstOrDefault(x => x.Code == code);
            if (plaqueInfo != null)
            {
                input.PlaqueId = plaqueInfo.Id;
                input.PreStateName = plaqueInfo.State.Name;
                input.PreStateId = plaqueInfo.State.Id;
                input.PlaqueHerdName = plaqueInfo.Livestock.Herd.HerdName;
            }
            else
            {
                input.PreStateName = "در انبار";
            }

            input.PlaqueCode = code.ToString();
            return input;
        }
    }
}