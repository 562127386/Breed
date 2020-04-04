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
using Akh.Breed.Plaques.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Plaques
{
    public class PlaqueChangeAppService :  BreedAppServiceBase, IPlaqueChangeAppService
    {
        private readonly IRepository<PlaqueChange> _plaqueChangeRepository;
        private readonly IRepository<PlaqueState> _plaqueStateRepository;
        
        public PlaqueChangeAppService(IRepository<PlaqueChange> plaqueChangeRepository, IRepository<PlaqueState> plaqueStateRepository)
        {
            _plaqueChangeRepository = plaqueChangeRepository;
            _plaqueStateRepository = plaqueStateRepository;
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
            var plaqueChange = ObjectMapper.Map<PlaqueChange>(input);
            await _plaqueChangeRepository.InsertAsync(plaqueChange);
        }
        
        private IQueryable<PlaqueChange> GetFilteredQuery(GetPlaqueChangeInput input)
        {
            long tempSearch = Convert.ToInt64(input.Filter);
            var query = QueryableExtensions.WhereIf(
                _plaqueChangeRepository.GetAll()
                    .Include(x => x.Plaque)
                    .Include(x => x.PreState)
                    .Include(x => x.NewState)
                    .Include(x => x.Officer),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Plaque.Code.ToString().Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(PlaqueChangeCreateOrUpdateInput input)
        {
            /*var species = _speciesinforepository.get(input.speciesid.value);
            if ( input.fromcode < species.fromcode)
            {
                input.fromcode += species.fromcode;
                input.tocode += species.fromcode;

            }

            if ( input.fromcode < species.fromcode || input.fromcode > species.tocode || input.tocode < species.fromcode || input.tocode > species.tocode)
            {
                throw new userfriendlyexception(l("thiscoderangeshouldbe", species.name,species.fromcode, species.tocode));
            }
            var existingobj = (await _plaquechangerepository.getall().asnotracking()
                .firstordefaultasync(u => 
                    (u.id != input.id) &&
                    ((u.fromcode.compareto(input.fromcode) <= 0 &&
                     u.tocode.compareto(input.fromcode) >= 0) ||
                    (u.fromcode.compareto(input.tocode) <= 0 &&
                     u.tocode.compareto(input.tocode) >= 0))));
            if (existingobj != null && existingobj.id != input.id)
            {
                throw new userfriendlyexception(l("thiscoderangehasoverlap",existingobj.fromcode, existingobj.tocode));
            }*/
            
        }
    }
}