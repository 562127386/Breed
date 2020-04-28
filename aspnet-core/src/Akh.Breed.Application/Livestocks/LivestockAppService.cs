using System;
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
using Abp.Runtime.Session;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors;
using Akh.Breed.Herds;
using Akh.Breed.Livestocks.Dto;
using Akh.Breed.Officers;
using Akh.Breed.Plaques;
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
        private readonly IRepository<Officer> _officerRepository;
        private readonly IRepository<PlaqueToOfficer> _plaqueToOfficerRepository;
        private readonly IRepository<PlaqueInfo, long> _plaqueInfoRepository;
        
        public LivestockAppService(IRepository<Livestock> livestockRepository, IRepository<SpeciesInfo> speciesInfoRepository, IRepository<SexInfo> sexInfoRepository, IRepository<Herd> herdRepository, IRepository<ActivityInfo> activityInfoRepository, IRepository<Officer> officerRepository, IRepository<PlaqueToOfficer> plaqueToOfficerRepository, IRepository<PlaqueInfo, long> plaqueInfoRepository)
        {
            _livestockRepository = livestockRepository;
            _speciesInfoRepository = speciesInfoRepository;
            _sexInfoRepository = sexInfoRepository;
            _herdRepository = herdRepository;
            _activityInfoRepository = activityInfoRepository;
            _officerRepository = officerRepository;
            _plaqueToOfficerRepository = plaqueToOfficerRepository;
            _plaqueInfoRepository = plaqueInfoRepository;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification)]
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
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification_Create, AppPermissions.Pages_IdentityInfo_Identification_Edit)]
        public async Task<GetLivestockForEditOutput> GetLivestockForEdit(NullableIdDto<int> input)
        {
            Livestock livestock = null;
            if (input.Id.HasValue)
            {
                livestock = await _livestockRepository.GetAll()
                    .Include(x => x.Officer)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new GetLivestockForEditOutput();
            
            //livestock
            var newLiveStock = new LivestockCreateOrUpdateInput();
            newLiveStock.CreationTime = newLiveStock.CreationTime.GetShamsi();
            output.Livestock = livestock != null
                ? ObjectMapper.Map<LivestockCreateOrUpdateInput>(livestock)
                : newLiveStock;
            
            //FirmTypes
            output.SpeciesInfos = _speciesInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Code + " - " + c.Name ))
                .ToList();

           output.SexInfos = _sexInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
           
            output.Herds = _herdRepository
                .GetAllList()
                .Where(x => x.CreatorUserId ==  AbpSession.UserId)
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Code + " (" +c.Name+","+c.Family+")" ))
                .ToList();

            if (output.Livestock.HerdId.HasValue)
            {
                output.ActivityInfos = _herdRepository.GetAll()
                    .Include(x => x.ActivityInfo)
                    .Where(x => x.Id == output.Livestock.HerdId)
                    .Select(c => new ComboboxItemDto(c.ActivityInfo.Id.ToString(), c.ActivityInfo.Name))
                    .ToList();
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification_Create, AppPermissions.Pages_IdentityInfo_Identification_Edit)]
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
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification_Delete)]
        public async Task DeleteLivestock(EntityDto input)
        {
            try
            {
                await _livestockRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification_Edit)]
        private async Task UpdateLivestockAsync(LivestockCreateOrUpdateInput input)
        {
            var livestock = ObjectMapper.Map<Livestock>(input);
            await _livestockRepository.UpdateAsync(livestock);
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification_Create)]
        private async Task CreateLivestockAsync(LivestockCreateOrUpdateInput input)
        {
            var livestock = ObjectMapper.Map<Livestock>(input);
            await _livestockRepository.InsertAsync(livestock);
            
            await CurrentUnitOfWork.SaveChangesAsync();
            
            var plaqueInfo = new PlaqueInfo
            {
                Code =  Convert.ToInt64(livestock.NationalCode),
                SetTime = livestock.CreationTime,
                Latitude = livestock.Latitude,
                Longitude = livestock.Longitude,
                OfficerId = livestock.OfficerId,
                StateId = 1,
                LivestockId = livestock.Id
            };
            await _plaqueInfoRepository.InsertAsync(plaqueInfo);
        }
        
        private IQueryable<Livestock> GetFilteredQuery(GetLivestockInput input)
        {
            var tUser = UserManager.GetUserById(AbpSession.GetUserId());
            var isOfficer = UserManager.IsInRoleAsync(tUser,StaticRoleNames.Host.Officer).Result;
            
            var query = QueryableExtensions.WhereIf(
                _livestockRepository.GetAll()
                .Include(x => x.SpeciesInfo)
                .Include(x => x.SexInfo)
                .Include(x => x.Herd)
                .Include(x => x.ActivityInfo)
                .Include(x => x.Officer),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.NationalCode.Contains(input.Filter));

            if (isOfficer)
            {
                query = query.Where(x => x.CreatorUserId == AbpSession.UserId);
            }
            
            return query;
        }        
        
        public async Task<LivestockCreateOrUpdateInput> CheckValidation(LivestockCreateOrUpdateInput input)
        {
            var species = await _speciesInfoRepository.GetAsync(input.SpeciesInfoId.Value);
            long nationalCode = Convert.ToInt64(input.NationalCode);
            if ( nationalCode < species.FromCode)
            {
                nationalCode += species.FromCode;
            }
            if ( nationalCode < species.FromCode || nationalCode > species.ToCode)
            {
                throw new UserFriendlyException(L("ThisCodeRangeShouldBe", species.Name,species.FromCode, species.ToCode));
            }

            var plaqueInfo = _plaqueInfoRepository.FirstOrDefault(x => x.Code == nationalCode);
            if ( plaqueInfo != null)
            {
                throw new UserFriendlyException(L("ThisCodeIsAllocated",nationalCode));
            }
            
            var officer = _officerRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
            if (officer == null)
            {
                throw new UserFriendlyException(L("TheOfficerDoesNotExists"));
            }
            else
            {
                input.OfficerId = officer.Id;
                var plaqueToOfficer = _plaqueToOfficerRepository.FirstOrDefault(x =>x.FromCode <= nationalCode && x.ToCode >= nationalCode);
                if (plaqueToOfficer == null)
                {
                    throw new UserFriendlyException(L("ThisStoreIsNotAllocatedTo", nationalCode));
                }
            }



            input.NationalCode = nationalCode.ToString();
            return input;
        }
        
        public List<ComboboxItemDto> GetActivityForCombo(NullableIdDto<int> input)
        {
            var query = _herdRepository.GetAll()
                .Include(x => x.ActivityInfo)
                .Where(x => x.Id == input.Id);

            return query.Select(c => new ComboboxItemDto(c.ActivityInfo.Id.ToString(), c.ActivityInfo.Name))
                .ToList();
        }
    }
}