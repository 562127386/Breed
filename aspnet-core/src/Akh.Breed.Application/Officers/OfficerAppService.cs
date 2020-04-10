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
using Abp.UI;
using Abp.Zero.Configuration;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Users;
using Akh.Breed.Authorization.Users.Dto;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors;
using Akh.Breed.Officers.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Officers
{
    public class OfficerAppService : BreedAppServiceBase, IOfficerAppService
    {
        private readonly IRepository<Officer> _officerRepository;
        private readonly IRepository<AcademicDegree> _academicDegreeRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<Contractor> _contractorRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public OfficerAppService(IRepository<Officer> officerRepository, IRepository<AcademicDegree> academicDegreeRepository, IRepository<StateInfo> stateInfoRepository, IRepository<Contractor> contractorRepository, IPasswordHasher<User> passwordHasher)
        {
            _officerRepository = officerRepository;
            _academicDegreeRepository = academicDegreeRepository;
            _stateInfoRepository = stateInfoRepository;
            _contractorRepository = contractorRepository;
            _passwordHasher = passwordHasher;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer)]
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
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer_Create, AppPermissions.Pages_BaseIntro_Officer_Edit)]
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
            
            //AcademicDegrees
            output.AcademicDegrees = _academicDegreeRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ){ IsSelected = output.Officer.AcademicDegreeId == c.Id })
                .ToList();
            
            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ){ IsSelected = output.Officer.StateInfoId == c.Id })
                .ToList();
            
            //Contractors
            output.Contractors = _contractorRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.FirmName + " (" +c.Name+","+c.Family+")" ){ IsSelected = output.Officer.ContractorId == c.Id })
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer_Create, AppPermissions.Pages_BaseIntro_Officer_Edit)]
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
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer_Delete)]
        public async Task DeleteOfficer(EntityDto input)
        {
            try
            {
                await _officerRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer_Edit)]
        private async Task UpdateOfficerAsync(OfficerCreateOrUpdateInput input)
        {
            var officer = ObjectMapper.Map<Officer>(input);
            await _officerRepository.UpdateAsync(officer);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer_Create)]
        private async Task CreateOfficerAsync(OfficerCreateOrUpdateInput input)
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

            long userId = user.ToUserIdentifier().UserId;
            if (userId > 0)
            {
                var officer = ObjectMapper.Map<Officer>(input);
                officer.UserId = userId;
                await _officerRepository.InsertAsync(officer);
            }
            else
            {
                throw new UserFriendlyException(L("AnErrorOccurred"));
            }
        }
        
        private IQueryable<Officer> GetFilteredQuery(GetOfficerInput input)
        {
            var query = QueryableExtensions.WhereIf(_officerRepository.GetAll()
                    .Include(x => x.Contractor)
                    .Include(x => x.StateInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Family.Contains(input.Filter) ||
                    u.NationalCode.Replace("-","").Contains(input.Filter) ||
                    u.MobileNumber.Replace("-","").Contains(input.Filter) ||
                    u.Contractor.Name.Contains(input.Filter) ||
                    u.Contractor.Family.Contains(input.Filter) ||
                    u.Contractor.FirmName.Contains(input.Filter) ||
                    u.StateInfo.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }        
   }
}