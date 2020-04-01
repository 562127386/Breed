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
using Akh.Breed.Contractors.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Contractors
{
    public class ContractorAppService : BreedAppServiceBase, IContractorAppService
    {
        private readonly IRepository<Contractor> _contractorRepository;
        private readonly IRepository<FirmType> _firmTypeRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<CityInfo> _cityInfoRepository;
        private readonly IRepository<RegionInfo> _regionInfoRepository;
        private readonly IRepository<VillageInfo> _villageInfoRepository;
        private readonly IRepository<UnionInfo> _unionInfoRepository;

        public ContractorAppService(IRepository<Contractor> contractorRepository, IRepository<FirmType> firmTypeRepository, IRepository<StateInfo> stateInfoRepository, IRepository<CityInfo> cityInfoRepository, IRepository<RegionInfo> regionInfoRepository, IRepository<VillageInfo> villageInfoRepository, IRepository<UnionInfo> unionInfoRepository)
        {
            _contractorRepository = contractorRepository;
            _firmTypeRepository = firmTypeRepository;
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
            _regionInfoRepository = regionInfoRepository;
            _villageInfoRepository = villageInfoRepository;
            _unionInfoRepository = unionInfoRepository;
        }
        public async Task<PagedResultDto<ContractorListDto>> GetContractor(GetContractorInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var contractors = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var contractorsListDto = ObjectMapper.Map<List<ContractorListDto>>(contractors);
            return new PagedResultDto<ContractorListDto>(
                userCount,
                contractorsListDto
            );
        }
        
        public async Task<GetContractorForEditOutput> GetContractorForEdit(NullableIdDto<int> input)
        {
            Contractor contractor = null;
            if (input.Id.HasValue)
            {
                contractor = await _contractorRepository.GetAll()
                    .Include(x => x.VillageInfo)
                    .Include(x => x.RegionInfo)
                    .Include(x => x.CityInfo)
                    .Include(x => x.StateInfo)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new GetContractorForEditOutput();
            
            //contractor
            output.Contractor = contractor != null
                ? ObjectMapper.Map<ContractorCreateOrUpdateInput>(contractor)
                : new ContractorCreateOrUpdateInput();
            
            //FirmTypes
            output.FirmTypes = _firmTypeRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ){ IsSelected = output.Contractor.FirmTypeId.Equals(c.Id) })
                .ToList();

            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            if (output.Contractor.StateInfoId.HasValue)
            {
                output.CityInfos = _cityInfoRepository.GetAll()
                    .Where(x => x.StateInfoId == output.Contractor.StateInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            if (output.Contractor.CityInfoId.HasValue)
            {
                output.RegionInfos = _regionInfoRepository.GetAll()
                    .Where(x => x.CityInfoId == output.Contractor.CityInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            if (output.Contractor.RegionInfoId.HasValue)
            {
                output.VillageInfos = _villageInfoRepository.GetAll()
                    .Where(x => x.RegionInfoId == output.Contractor.RegionInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            output.UnionInfos = _unionInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            return output;
        }
        
        public async Task CreateOrUpdateContractor(ContractorCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateContractorAsync(input);
            }
            else
            {
                await CreateContractorAsync(input);
            }
        }
        
        public async Task DeleteContractor(EntityDto input)
        {
            try
            {
                await _contractorRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        private async Task UpdateContractorAsync(ContractorCreateOrUpdateInput input)
        {
            var contractor = ObjectMapper.Map<Contractor>(input);
            await _contractorRepository.UpdateAsync(contractor);
        }
        
        private async Task CreateContractorAsync(ContractorCreateOrUpdateInput input)
        {
            var contractor = ObjectMapper.Map<Contractor>(input);
            await _contractorRepository.InsertAsync(contractor);
        }
        
        private IQueryable<Contractor> GetFilteredQuery(GetContractorInput input)
        {
            var query = QueryableExtensions.WhereIf(
                _contractorRepository.GetAll()
                    .Include(x => x.FirmType)
                    .Include(x => x.StateInfo)
                    .Include(x => x.CityInfo)
                    .Include(x => x.RegionInfo)
                    .Include(x => x.VillageInfo)
                    .Include(x => x.UnionInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Family.Contains(input.Filter) ||
                    u.StateInfo.Name.Contains(input.Filter) ||
                    u.CityInfo.Name.Contains(input.Filter) ||
                    u.RegionInfo.Name.Contains(input.Filter) ||
                    u.VillageInfo.Name.Contains(input.Filter) ||
                    u.UnionInfo.Name.Contains(input.Filter) ||
                    u.NationalCode.Replace("-","").Contains(input.Filter) ||
                    u.FirmName.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }        
   }
}