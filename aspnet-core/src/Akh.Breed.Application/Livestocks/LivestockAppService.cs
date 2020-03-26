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
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors;
using Akh.Breed.Herds;
using Akh.Breed.Livestocks.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Livestocks
{
    public class LivestockAppService : BreedAppServiceBase, ILivestockAppService
    {
        private readonly IRepository<Livestock> _livestockRepository;
        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;
        private readonly IRepository<SexInfo> _sexInfoRepository;
        private readonly IRepository<Herd> _herdRepository;
        private readonly IRepository<ActivityInfo> _activityInfoRepository;
        
        public LivestockAppService(IRepository<Livestock> livestockRepository, IRepository<SpeciesInfo> speciesInfoRepository, IRepository<SexInfo> sexInfoRepository, IRepository<Herd> herdRepository, IRepository<ActivityInfo> activityInfoRepository)
        {
            _livestockRepository = livestockRepository;
            _speciesInfoRepository = speciesInfoRepository;
            _sexInfoRepository = sexInfoRepository;
            _herdRepository = herdRepository;
            _activityInfoRepository = activityInfoRepository;
        }
        public async Task<PagedResultDto<LivestockListDto>> GetLivestock(GetLivestockInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var livestocks = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var livestocksListDto = ObjectMapper.Map<List<LivestockListDto>>(livestocks);
            return new PagedResultDto<LivestockListDto>(
                userCount,
                livestocksListDto
            );
        }
        
        public async Task<GetLivestockForEditOutput> GetLivestockForEdit(NullableIdDto<int> input)
        {
            Livestock livestock = null;
            if (input.Id.HasValue)
            {
                livestock = await _livestockRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new GetLivestockForEditOutput();
            
            //livestock
            output.Livestock = livestock != null
                ? ObjectMapper.Map<LivestockCreateOrUpdateInput>(livestock)
                : new LivestockCreateOrUpdateInput();
            
            //FirmTypes
            output.SpeciesInfos = _speciesInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Code + " - " + c.Name ))
                .ToList();

           output.SexInfos = _sexInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            output.ActivityInfos = _activityInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            output.Herds = _herdRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Code + " (" +c.Name+","+c.Family+")" ))
                .ToList();
            
            return output;
        }
        
        public async Task CreateOrUpdateLivestock(LivestockCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateLivestockAsync(input);
            }
            else
            {
                await CreateLivestockAsync(input);
            }
        }
        
        public async Task DeleteLivestock(EntityDto input)
        {
            await _livestockRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateLivestockAsync(LivestockCreateOrUpdateInput input)
        {
            var livestock = ObjectMapper.Map<Livestock>(input);
            await _livestockRepository.UpdateAsync(livestock);
        }
        
        private async Task CreateLivestockAsync(LivestockCreateOrUpdateInput input)
        {
            var livestock = ObjectMapper.Map<Livestock>(input);
            await _livestockRepository.InsertAsync(livestock);
        }
        
        private IQueryable<Livestock> GetFilteredQuery(GetLivestockInput input)
        {
            var query = QueryableExtensions.WhereIf(
                _livestockRepository.GetAll()
                .Include(x => x.SpeciesInfo)
                .Include(x => x.SexInfo)
                .Include(x => x.Herd)
                .Include(x => x.ActivityInfo)
                .Include(x => x.Officer),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.NationalCode.Contains(input.Filter));

            return query;
        }        
        
        private async Task CheckValidation(LivestockCreateOrUpdateInput input)
        {
            var species = _speciesInfoRepository.Get(input.SpeciesInfoId.Value);
            long nationalCode = Convert.ToInt64(input.NationalCode);
            if ( nationalCode < species.FromCode || nationalCode > species.ToCode)
            {
                throw new UserFriendlyException(L("ThisCodeRangeShouldBe", species.Name,species.FromCode, species.ToCode));
            }

            //throw new UserFriendlyException(L("ThisCodeRangeHasOverlap",existingObj.FromCode, existingObj.ToCode));

        }
   }
}