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
using Abp.Timing;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors;
using Akh.Breed.Herds.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Herds
{
    public class HerdGeoLogAppService : BreedAppServiceBase, IHerdGeoLogAppService
    {
        private readonly IRepository<HerdGeoLog> _herdGeoLogRepository;
        private readonly IRepository<Herd> _herdRepository;
        
        public HerdGeoLogAppService(IRepository<HerdGeoLog> herdGeoLogRepository, IRepository<Herd> herdRepository)
        {
            _herdGeoLogRepository = herdGeoLogRepository;
            _herdRepository = herdRepository;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Activities_EditGeoHerd)]
        public async Task<PagedResultDto<HerdGeoLogListDto>> GetHerdGeoLog(GetHerdGeoLogInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var herdGeoLogs = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var herdGeoLogsListDto = ObjectMapper.Map<List<HerdGeoLogListDto>>(herdGeoLogs);
            return new PagedResultDto<HerdGeoLogListDto>(
                userCount,
                herdGeoLogsListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_Activities_EditGeoHerd, AppPermissions.Pages_Activities_EditGeoHerd_Create, AppPermissions.Pages_Activities_EditGeoHerd_Edit)]
        public async Task<GetHerdGeoLogForEditOutput> GetHerdGeoLogForEdit(NullableIdDto<int> input)
        {
            HerdGeoLog herdGeoLog = null;
            if (input.Id.HasValue)
            {
                herdGeoLog = await _herdGeoLogRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new GetHerdGeoLogForEditOutput();
            
            //herdGeoLog
            output.HerdGeoLog = herdGeoLog != null
                ? ObjectMapper.Map<HerdGeoLogCreateOrUpdateInput>(herdGeoLog)
                : new HerdGeoLogCreateOrUpdateInput();
            
           //StateInfos
            output.Herds = _herdRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Code + " - " + c.HerdName + "(" +c.Name+","+c.Family+")" ))
                .ToList();
            
            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Activities_EditGeoHerd_Create, AppPermissions.Pages_Activities_EditGeoHerd_Edit)]
        public async Task CreateOrUpdateHerdGeoLog(HerdGeoLogCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateHerdGeoLogAsync(input);
            }
            else
            {
                await CreateHerdGeoLogAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_Activities_EditGeoHerd_Delete)]
        public async Task DeleteHerdGeoLog(EntityDto input)
        {
            try
            {
                await _herdGeoLogRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Activities_EditGeoHerd_Edit)]
        private async Task UpdateHerdGeoLogAsync(HerdGeoLogCreateOrUpdateInput input)
        {
            var herdGeoLog = ObjectMapper.Map<HerdGeoLog>(input);
            await _herdGeoLogRepository.UpdateAsync(herdGeoLog);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Activities_EditGeoHerd_Create)]
        private async Task CreateHerdGeoLogAsync(HerdGeoLogCreateOrUpdateInput input)
        {
            var herd = _herdRepository.Get(input.HerdId.Value);
            var herdGeoLog = ObjectMapper.Map<HerdGeoLog>(input);
            
            herd.Latitude = input.Latitude;
            herd.Longitude = input.Longitude;
            herd.CreationTime = Clock.Now;
            await _herdRepository.UpdateAsync(herd);
            await _herdGeoLogRepository.InsertAsync(herdGeoLog);
        }
        
        private IQueryable<HerdGeoLog> GetFilteredQuery(GetHerdGeoLogInput input)
        {
            var query = QueryableExtensions.WhereIf(
                _herdGeoLogRepository.GetAll()
                .Include(x => x.Herd)
                .Include(x => x.Officer),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Herd.HerdName.Contains(input.Filter) ||
                    u.Herd.FirmName.Contains(input.Filter));

            return query;
        }        
   }
}