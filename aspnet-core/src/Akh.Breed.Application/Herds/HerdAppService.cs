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
using Akh.Breed.Herds.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Herds
{
    public class HerdAppService : BreedAppServiceBase, IHerdAppService
    {
        private readonly IRepository<Herd> _herdRepository;
        private readonly IRepository<EpidemiologicInfo> _epidemiologicInfoRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<CityInfo> _cityInfoRepository;
        private readonly IRepository<RegionInfo> _regionInfoRepository;
        private readonly IRepository<VillageInfo> _villageInfoRepository;
        private readonly IRepository<UnionInfo> _unionInfoRepository;
        private readonly IRepository<ActivityInfo> _activityInfoRepository;
        private readonly IRepository<Contractor> _contractorRepository;
        
        public HerdAppService(IRepository<Herd> herdRepository, IRepository<StateInfo> stateInfoRepository, IRepository<CityInfo> cityInfoRepository, IRepository<RegionInfo> regionInfoRepository, IRepository<VillageInfo> villageInfoRepository, IRepository<UnionInfo> unionInfoRepository, IRepository<EpidemiologicInfo> epidemiologicInfoRepository, IRepository<ActivityInfo> activityInfoRepository, IRepository<Contractor> contractorRepository)
        {
            _herdRepository = herdRepository;
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
            _regionInfoRepository = regionInfoRepository;
            _villageInfoRepository = villageInfoRepository;
            _unionInfoRepository = unionInfoRepository;
            _epidemiologicInfoRepository = epidemiologicInfoRepository;
            _activityInfoRepository = activityInfoRepository;
            _contractorRepository = contractorRepository;
        }
        public async Task<PagedResultDto<HerdListDto>> GetHerd(GetHerdInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var herds = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var herdsListDto = ObjectMapper.Map<List<HerdListDto>>(herds);
            return new PagedResultDto<HerdListDto>(
                userCount,
                herdsListDto
            );
        }
        
        public async Task<GetHerdForEditOutput> GetHerdForEdit(NullableIdDto<int> input)
        {
            Herd herd = null;
            if (input.Id.HasValue)
            {
                herd = await _herdRepository.GetAll()
                    .Include(x => x.VillageInfo)
                    .ThenInclude(x => x.RegionInfo)
                    .ThenInclude(x => x.CityInfo)
                    .ThenInclude(x => x.StateInfo)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new GetHerdForEditOutput();
            
            //herd
            output.Herd = herd != null
                ? ObjectMapper.Map<HerdCreateOrUpdateInput>(herd)
                : new HerdCreateOrUpdateInput();
            
            //FirmTypes
            output.EpidemiologicInfos = _epidemiologicInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Code + " - " + c.Name ))
                .ToList();

            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            if (output.Herd.StateInfoId.HasValue)
            {
                output.CityInfos = _cityInfoRepository.GetAll()
                    .Where(x => x.StateInfoId == output.Herd.StateInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            if (output.Herd.CityInfoId.HasValue)
            {
                output.RegionInfos = _regionInfoRepository.GetAll()
                    .Where(x => x.CityInfoId == output.Herd.CityInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            if (output.Herd.RegionInfoId.HasValue)
            {
                output.VillageInfos = _villageInfoRepository.GetAll()
                    .Where(x => x.RegionInfoId == output.Herd.RegionInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
            }
            
            output.UnionInfos = _unionInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            output.ActivityInfos = _activityInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            output.Contractors = _contractorRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.FirmName + " (" +c.Name+","+c.Family+")" ))
                .ToList();
            
            return output;
        }
        
        public async Task CreateOrUpdateHerd(HerdCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateHerdAsync(input);
            }
            else
            {
                await CreateHerdAsync(input);
            }
        }
        
        public async Task DeleteHerd(EntityDto input)
        {
            await _herdRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateHerdAsync(HerdCreateOrUpdateInput input)
        {
            var herd = ObjectMapper.Map<Herd>(input);
            await _herdRepository.UpdateAsync(herd);
        }
        
        private async Task CreateHerdAsync(HerdCreateOrUpdateInput input)
        {
            var herd = ObjectMapper.Map<Herd>(input);
            await _herdRepository.InsertAsync(herd);
        }
        
        private IQueryable<Herd> GetFilteredQuery(GetHerdInput input)
        {
            var query = QueryableExtensions.WhereIf(
                _herdRepository.GetAll()
                .Include(x => x.Contractor)
                .Include(x => x.ActivityInfo)
                .Include(x => x.EpidemiologicInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }        
   }
}