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
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.Authorization.Users;
using Akh.Breed.Contractors;
using Akh.Breed.Unions.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Unions
{
    public class UnionEmployeeAppService :  BreedAppServiceBase, IUnionEmployeeAppService
    {
        private readonly IRepository<UnionEmployee> _unionEmployeeRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleManager _roleManager;

        public UnionEmployeeAppService(IRepository<UnionEmployee> unionEmployeeRepository, IPasswordHasher<User> passwordHasher, RoleManager roleManager)
        {
            _unionEmployeeRepository = unionEmployeeRepository;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionEmployee)]
        public async Task<PagedResultDto<UnionEmployeeListDto>> GetUnionEmployee(GetUnionEmployeeInput input)
        {
            var query = GetFilteredQuery(input);
            query = query.Where(x => x.UnionInfoId == input.UnionInfoId);
            var userCount = await query.CountAsync();
            var unionEmployees = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var unionEmployeesListDto = ObjectMapper.Map<List<UnionEmployeeListDto>>(unionEmployees);
            return new PagedResultDto<UnionEmployeeListDto>(
                userCount,
                unionEmployeesListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionEmployee_Create, AppPermissions.Pages_BaseIntro_UnionEmployee_Edit)]
        public async Task<UnionEmployeeCreateOrUpdateInput> GetUnionEmployeeForEdit(int unionInfoId, NullableIdDto<int> input)
        {
            var output = new UnionEmployeeCreateOrUpdateInput();
            output.UnionInfoId = unionInfoId;
            if (input.Id.HasValue)
            {
                var unionEmployee = await _unionEmployeeRepository.GetAll()
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (unionEmployee != null)
                    ObjectMapper.Map<UnionEmployee,UnionEmployeeCreateOrUpdateInput>(unionEmployee,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionEmployee, AppPermissions.Pages_BaseIntro_UnionEmployee_Create, AppPermissions.Pages_BaseIntro_UnionEmployee_Edit)]
        public async Task CreateOrUpdateUnionEmployee(UnionEmployeeCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateUnionEmployeeAsync(input);
            }
            else
            {
                await CreateUnionEmployeeAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionEmployee_Delete)]
        public async Task DeleteUnionEmployee(EntityDto input)
        {
            try
            {
                var unionEmployee = _unionEmployeeRepository.GetAll()
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.Id == input.Id);
                await UserManager.DeleteAsync(unionEmployee?.User);
                await _unionEmployeeRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionEmployee_Edit)]
        private async Task UpdateUnionEmployeeAsync(UnionEmployeeCreateOrUpdateInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            user.Name = input.Name;
            user.Surname = input.Family;
            user.UserName = input.UserName;
            user.EmailAddress = input.UserName + "@mgnsys.ir";
            CheckErrors(await UserManager.UpdateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();
            
            var unionEmployee = ObjectMapper.Map<UnionEmployee>(input);
            await _unionEmployeeRepository.UpdateAsync(unionEmployee);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionEmployee_Create)]
        private async Task CreateUnionEmployeeAsync(UnionEmployeeCreateOrUpdateInput input)
        {
            var nationalCode = input.NationalCode.Replace("-", "");
            var user = new User
            {
                IsActive = true,
                ShouldChangePasswordOnNextLogin = true,
                UserName = input.UserName,
                EmailAddress = input.UserName + "@mgnsys.ir",
                Name = input.Name,
                Surname = input.Family
            };
            
            user.Password = _passwordHasher.HashPassword(user, nationalCode);
            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();
            var officerRole = _roleManager.GetRoleByName(StaticRoleNames.Host.StateAdmin);
            long userId = user.ToUserIdentifier().UserId;
            user.Roles = new List<UserRole>();
            user.Roles.Add(new UserRole(null, user.Id, officerRole.Id));

            if (userId > 0)
            {
                var unionEmployee = ObjectMapper.Map<UnionEmployee>(input);
                unionEmployee.UserId = userId;
                await _unionEmployeeRepository.InsertAsync(unionEmployee);
            }
            else
            {
                throw new UserFriendlyException(L("AnErrorOccurred"));
            }
            
        }
        
        private IQueryable<UnionEmployee> GetFilteredQuery(GetUnionEmployeeInput input)
        {
            var query = QueryableExtensions.WhereIf(_unionEmployeeRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Family.Contains(input.Filter) ||
                    u.NationalCode.Replace("-","").Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(UnionEmployeeCreateOrUpdateInput input)
        {

        }
    }
}