using System.Collections.Generic;
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
using Akh.Breed.Contractors;
using Akh.Breed.Officers.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Officers
{
    public class OfficerAppService : BreedAppServiceBase, IOfficerAppService
    {
        private readonly IRepository<Officer> _officerRepository;
        private readonly IRepository<AcademicDegree> _academicDegreeRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<Contractor> _contractorRepository;

        public OfficerAppService(IRepository<Officer> officerRepository, IRepository<AcademicDegree> academicDegreeRepository, IRepository<StateInfo> stateInfoRepository, IRepository<Contractor> contractorRepository)
        {
            _officerRepository = officerRepository;
            _academicDegreeRepository = academicDegreeRepository;
            _stateInfoRepository = stateInfoRepository;
            _contractorRepository = contractorRepository;
        }
        public async Task<PagedResultDto<OfficerListDto>> GetOfficer(GetOfficerInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var officers = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var officersListDto = ObjectMapper.Map<List<OfficerListDto>>(officers);
            return new PagedResultDto<OfficerListDto>(
                userCount,
                officersListDto
            );
        }
        
        public async Task<GetOfficerForEditOutput> GetOfficerForEdit(NullableIdDto<int> input)
        {
            Officer officer = null;
            if (input.Id.HasValue)
            {
                officer = await _officerRepository.GetAsync(input.Id.Value);
            }
            
            var output = new GetOfficerForEditOutput();
            
            //officer
            output.Officer = officer != null
                ? ObjectMapper.Map<OfficerEditDto>(officer)
                : new OfficerEditDto();
            
            //FirmTypes
            output.AcademicDegrees = _academicDegreeRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ){ IsSelected = output.Officer.AcademicDegreeId == c.Id })
                .ToList();
            
            //FirmTypes
            output.StateInfos = _stateInfoRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ){ IsSelected = output.Officer.StateInfoId == c.Id })
                .ToList();
            
            //FirmTypes
            output.Contractors = _contractorRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name + "/" + c.FirmName ){ IsSelected = output.Officer.ContractorId == c.Id })
                .ToList();

            return output;
        }
        
        public async Task CreateOrUpdateOfficer(OfficerCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateOfficerAsync(input);
            }
            else
            {
                await CreateOfficerAsync(input);
            }
        }
        
        public async Task DeleteOfficer(EntityDto input)
        {
            await _officerRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateOfficerAsync(OfficerCreateOrUpdateInput input)
        {
            var officer = ObjectMapper.Map<Officer>(input);
            await _officerRepository.UpdateAsync(officer);
        }
        
        private async Task CreateOfficerAsync(OfficerCreateOrUpdateInput input)
        {
            var officer = ObjectMapper.Map<Officer>(input);
            await _officerRepository.InsertAsync(officer);
        }
        
        private IQueryable<Officer> GetFilteredQuery(GetOfficerInput input)
        {
            var query = QueryableExtensions.WhereIf(_officerRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }        
   }
}