using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.BaseInfo;
using Akh.Breed.Contractors;
using Akh.Breed.Herds;
using Akh.Breed.Inseminating.Dto;
using Akh.Breed.Livestocks.Dto;
using Akh.Breed.Officers;
using Akh.Breed.Plaques;
using Akh.Breed.Unions;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Inseminating
{
    public class InseminationAppService : BreedAppServiceBase, IInseminationAppService
    {
        private readonly IRepository<Insemination> _inseminationRepository;
        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;
        private readonly IRepository<SexInfo> _sexInfoRepository;
        private readonly IRepository<Herd> _herdRepository;
        private readonly IRepository<ActivityInfo> _activityInfoRepository;
        private readonly IRepository<Officer> _officerRepository;
        private readonly IRepository<PlaqueToOfficer> _plaqueToOfficerRepository;
        private readonly IRepository<PlaqueInfo, long> _plaqueInfoRepository;
        private readonly IRepository<UnionInfo> _unionInfoRepository;
        private readonly IRepository<Contractor> _contractorRepository;
        private readonly IRepository<BreedInfo> _breedInfoRepository;
        private readonly IRepository<BirthTypeInfo> _birthTypeInfoRepository;
        private readonly IRepository<AnomalyInfo> _anomalyInfoRepository;
        private readonly IRepository<MembershipInfo> _membershipInfoRepository;
        private readonly IRepository<BodyColorInfo> _bodyColorInfoRepository;
        private readonly IRepository<SpotConnectorInfo> _spotConnectorInfoRepository;
        
        public InseminationAppService(IRepository<Insemination> inseminationRepository, IRepository<SpeciesInfo> speciesInfoRepository, IRepository<SexInfo> sexInfoRepository, IRepository<Herd> herdRepository, IRepository<ActivityInfo> activityInfoRepository, IRepository<Officer> officerRepository, IRepository<PlaqueToOfficer> plaqueToOfficerRepository, IRepository<PlaqueInfo, long> plaqueInfoRepository, IRepository<UnionInfo> unionInfoRepository, IRepository<Contractor> contractorRepository, IRepository<BreedInfo> breedInfoRepository, IRepository<BirthTypeInfo> birthTypeInfoRepository, IRepository<AnomalyInfo> anomalyInfoRepository, IRepository<MembershipInfo> membershipInfoRepository, IRepository<BodyColorInfo> bodyColorInfoRepository, IRepository<SpotConnectorInfo> spotConnectorInfoRepository)
        {
            _inseminationRepository = inseminationRepository;
            _speciesInfoRepository = speciesInfoRepository;
            _sexInfoRepository = sexInfoRepository;
            _herdRepository = herdRepository;
            _activityInfoRepository = activityInfoRepository;
            _officerRepository = officerRepository;
            _plaqueToOfficerRepository = plaqueToOfficerRepository;
            _plaqueInfoRepository = plaqueInfoRepository;
            _unionInfoRepository = unionInfoRepository;
            _contractorRepository = contractorRepository;
            _breedInfoRepository = breedInfoRepository;
            _birthTypeInfoRepository = birthTypeInfoRepository;
            _anomalyInfoRepository = anomalyInfoRepository;
            _membershipInfoRepository = membershipInfoRepository;
            _bodyColorInfoRepository = bodyColorInfoRepository;
            _spotConnectorInfoRepository = spotConnectorInfoRepository;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification)]
        public async Task<PagedResultDto<InseminationListDto>> GetInsemination(GetInseminationInput input)
        {
            var query = GetFilteredQuery(input);
            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());
            var isAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Admin);
            var isSysAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.SysAdmin);
            var isStateAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.StateAdmin);
            var isCityAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.CityAdmin);
            var isOfficer = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Officer);
            if (isAdmin || isSysAdmin)
            {
                query = query;
            }
            else if (isStateAdmin)
            {
                var union = _unionInfoRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                query = query.Where(x => x.Herd.UnionInfo.StateInfoId == union.StateInfoId);
            }
            else if (isCityAdmin)
            {
                var contractor = _contractorRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                query = query.Where(x => x.Herd.ContractorId == contractor.Id);
            }
            else if (isOfficer)
            {
                var officer = _officerRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                query = query.Where(x => x.OfficerId == officer.Id);
            }
            else
            {
                query = query.Where(x => false);
            }
            
            var userCount = await query.CountAsync();
            var inseminations = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var inseminationsListDto = ObjectMapper.Map<List<InseminationListDto>>(inseminations);
            return new PagedResultDto<InseminationListDto>(
                userCount,
                inseminationsListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification, AppPermissions.Pages_IdentityInfo_Identification_Create, AppPermissions.Pages_IdentityInfo_Identification_Edit)]
        public async Task<GetInseminationForEditOutput> GetInseminationForEdit(NullableIdDto<int> input)
        {
            var officer = _officerRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
            if (officer == null)
            {
                throw new UserFriendlyException(L("TheOfficerDoesNotExists"));
            }
            Insemination insemination = null;
            if (input.Id.HasValue)
            {
                insemination = await _inseminationRepository.GetAll()
                    .Include(x => x.Officer)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new GetInseminationForEditOutput();
            
            //insemination
            var newLiveStock = new InseminationCreateOrUpdateInput();
            newLiveStock.CreationTime = newLiveStock.CreationTime.GetShamsi();
            output.Insemination = insemination != null
                ? ObjectMapper.Map<InseminationCreateOrUpdateInput>(insemination)
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
               .Where(x => x.OfficerId ==  officer.Id && !x.IsCertificated)
               .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Code + " - " + c.HerdName + "(" +c.Name+","+c.Family+")" ))
               .ToList();
           
           output.BreedInfos = _breedInfoRepository
               .GetAllList()
               .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ))
               .ToList();
           
           output.BirthTypeInfos = _birthTypeInfoRepository
               .GetAllList()
               .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ))
               .ToList();
           
           output.AnomalyInfos = _anomalyInfoRepository
               .GetAllList()
               .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ))
               .ToList();
           
           output.MembershipInfos = _membershipInfoRepository
               .GetAllList()
               .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ))
               .ToList();
           
           output.BodyColorInfos = _bodyColorInfoRepository
               .GetAllList()
               .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ))
               .ToList();
           
           output.SpotConnectorInfos = _spotConnectorInfoRepository
               .GetAllList()
               .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ))
               .ToList();

            if (output.Insemination.HerdId.HasValue)
            {
                output.ActivityInfos = _herdRepository.GetAll()
                    .Include(x => x.ActivityInfo)
                    .Where(x => x.Id == output.Insemination.HerdId)
                    .Select(c => new ComboboxItemDto(c.ActivityInfo.Id.ToString(), c.ActivityInfo.Name))
                    .ToList();
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification_Create, AppPermissions.Pages_IdentityInfo_Identification_Edit)]
        public async Task CreateOrUpdateInsemination(InseminationCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateInseminationAsync(input);
            }
            else
            {
                await CreateInseminationAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification_Delete)]
        public async Task DeleteInsemination(EntityDto input)
        {
            try
            {
                await _inseminationRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification_Edit)]
        private async Task UpdateInseminationAsync(InseminationCreateOrUpdateInput input)
        {
            var insemination = ObjectMapper.Map<Insemination>(input);
            await _inseminationRepository.UpdateAsync(insemination);
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_Identification_Create)]
        private async Task CreateInseminationAsync(InseminationCreateOrUpdateInput input)
        {
            var insemination = ObjectMapper.Map<Insemination>(input);
            await _inseminationRepository.InsertAsync(insemination);
            
            await CurrentUnitOfWork.SaveChangesAsync();
            
            var plaqueInfo = new PlaqueInfo
            {
                Code =  Convert.ToInt64(insemination.NationalCode),
                SetTime = insemination.CreationTime,
                Latitude = insemination.Latitude,
                Longitude = insemination.Longitude,
                OfficerId = insemination.OfficerId,
                StateId = 1,
                InseminationId = insemination.Id
            };
            await _plaqueInfoRepository.InsertAsync(plaqueInfo);
        }
        
        private IQueryable<Insemination> GetFilteredQuery(GetInseminationInput input)
        {
            
            var query = QueryableExtensions.WhereIf(
                _inseminationRepository.GetAll()
                .Include(x => x.SpeciesInfo)
                .Include(x => x.SexInfo)
                .Include(x => x.Herd)
                .Include(x => x.ActivityInfo)
                .Include(x => x.Officer),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.NationalCode.Contains(input.Filter) || 
                    u.SpeciesInfo.Name.Contains(input.Filter)|| 
                    u.Herd.HerdName.Contains(input.Filter)|| 
                    u.Herd.Name.Contains(input.Filter)|| 
                    u.Herd.Family.Contains(input.Filter)|| 
                    u.ActivityInfo.Name.Contains(input.Filter)|| 
                    u.Officer.Name.Contains(input.Filter)|| 
                    u.Officer.Family.Contains(input.Filter));
                                
            return query;
        }        
        
        public async Task<InseminationCreateOrUpdateInput> CheckValidation(InseminationCreateOrUpdateInput input)
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

            var plaqueInfo = _plaqueInfoRepository.FirstOrDefault(x => x.Code == nationalCode && x.StateId != 5);
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