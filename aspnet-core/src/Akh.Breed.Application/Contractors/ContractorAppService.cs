using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.Authorization.Users;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors.Dto;
using Akh.Breed.Officers;
using Akh.Breed.Unions;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleManager _roleManager;


        public ContractorAppService(IRepository<Contractor> contractorRepository, IRepository<FirmType> firmTypeRepository, IRepository<StateInfo> stateInfoRepository, IRepository<CityInfo> cityInfoRepository, IRepository<RegionInfo> regionInfoRepository, IRepository<VillageInfo> villageInfoRepository, IRepository<UnionInfo> unionInfoRepository, RoleManager roleManager, IPasswordHasher<User> passwordHasher)
        {
            _contractorRepository = contractorRepository;
            _firmTypeRepository = firmTypeRepository;
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
            _regionInfoRepository = regionInfoRepository;
            _villageInfoRepository = villageInfoRepository;
            _unionInfoRepository = unionInfoRepository;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Contractor)]
        public async Task<PagedResultDto<ContractorListDto>> GetContractor(GetContractorInput input)
        {
            var query = GetFilteredQuery(input);
            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());
            var isAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Admin);
            var isSysAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.SysAdmin);
            var isStateAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.StateAdmin);
            if (isAdmin || isSysAdmin)
            {
                query = query;
            }
            else if (isStateAdmin)
            {
                var union = _unionInfoRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                query = query.Where(x => x.UnionInfoId == union.Id);
            }
            else
            {
                query = query.Where(x => false);
            }
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
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Contractor, AppPermissions.Pages_BaseIntro_Contractor_Create, AppPermissions.Pages_BaseIntro_Contractor_Edit)]
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
            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());
            var isAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Admin);
            var isSysAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.SysAdmin);
            var isStateAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.StateAdmin);
            var stateInfoQuery = _stateInfoRepository.GetAll();
            if (isAdmin || isSysAdmin)
            {
                stateInfoQuery = stateInfoQuery;
            }
            else if (isStateAdmin)
            {
                var union = _unionInfoRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                stateInfoQuery = stateInfoQuery.Where(x => x.Id == union.StateInfoId);
            }
            else
            {
                stateInfoQuery = stateInfoQuery.Where(x => false);
            }
            
            output.StateInfos = stateInfoQuery
                .ToList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            if (output.Contractor.StateInfoId.HasValue)
            {
                output.CityInfos = _cityInfoRepository.GetAll()
                    .Where(x => x.StateInfoId == output.Contractor.StateInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList();
                
                output.UnionInfos = _unionInfoRepository.GetAll()
                    .Where(x => x.StateInfoId == output.Contractor.StateInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.UnionName))
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

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Contractor_Create, AppPermissions.Pages_BaseIntro_Contractor_Edit)]
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
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Contractor_Delete)]
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

        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Contractor_Edit)]
        private async Task UpdateContractorAsync(ContractorCreateOrUpdateInput input)
        {
            var contractor = ObjectMapper.Map<Contractor>(input);
            await _contractorRepository.UpdateAsync(contractor);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Contractor_Create)]
        private async Task CreateContractorAsync(ContractorCreateOrUpdateInput input)
        {
            var nationalCode = input.NationalCode.Replace("-", "");
            var user = new User
            {
                IsActive = true,
                ShouldChangePasswordOnNextLogin = true,
                UserName = nationalCode,
                EmailAddress = nationalCode + "@mgnsys.ir",
                Name = input.Name,
                Surname = input.Family
            };
            
            user.Password = _passwordHasher.HashPassword(user, nationalCode);
            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();
            var officerRole = _roleManager.GetRoleByName(StaticRoleNames.Host.CityAdmin);
            long userId = user.ToUserIdentifier().UserId;
            user.Roles = new List<UserRole>();
            user.Roles.Add(new UserRole(null, user.Id, officerRole.Id));

            if (userId > 0)
            {
                var contractor = ObjectMapper.Map<Contractor>(input);
                contractor.UserId = userId;
                await _contractorRepository.InsertAsync(contractor);;
            }
            else
            {
                throw new UserFriendlyException(L("AnErrorOccurred"));
            }
            
        }
        
        public List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input)
        {
            var query = _contractorRepository.GetAll();
            if (input.Id.HasValue)
            {
                query = query.Where(x => x.StateInfoId == input.Id);
            }
            
            return query.Select(c => new ComboboxItemDto(c.Id.ToString(), c.FirmName + " (" + c.Name + "," + c.Family + ")"))
                .ToList();
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
                    u.UnionInfo.UnionName.Contains(input.Filter) ||
                    u.NationalCode.Replace("-","").Contains(input.Filter) ||
                    u.FirmName.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }        
   }
}