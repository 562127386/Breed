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
using Akh.Breed.Support.Dto;
using Akh.Breed.Officers;
using Akh.Breed.Plaques;
using Akh.Breed.Unions;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Support
{
    public class SupportAppService : BreedAppServiceBase, ISupportAppService
    {
        private readonly IRepository<Support> _supportRepository;
        private readonly IRepository<SupportType> _supportTypeRepository;
        private readonly IRepository<SupportState> _supportStateRepository;
        
        public SupportAppService(IRepository<Support> supportRepository, IRepository<SupportType> supportTypeRepository, IRepository<SupportState> supportStateRepository)
        {
            _supportRepository = supportRepository;
            _supportTypeRepository = supportTypeRepository;
            _supportStateRepository = supportStateRepository;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Administration_Support)]
        public async Task<PagedResultDto<SupportListDto>> GetSupport(GetSupportInput input)
        {
            var query = GetFilteredQuery(input);
            
            var userCount = await query.CountAsync();
            var supports = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var supportsListDto = ObjectMapper.Map<List<SupportListDto>>(supports);
            return new PagedResultDto<SupportListDto>(
                userCount,
                supportsListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_Administration_Support, AppPermissions.Pages_Administration_Support_Create, AppPermissions.Pages_Administration_Support_Edit)]
        public async Task<SupportGetForEditOutput> GetSupportForEdit(NullableIdDto<int> input)
        {
            Support support = null;
            if (input.Id.HasValue)
            {
                support = await _supportRepository.GetAll()
                    .Include(x => x.SupportType)
                    .Include(x => x.SupportState)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new SupportGetForEditOutput();
            
            //support
            var newSupport = new SupportCreateOrUpdateInput();
            newSupport.CreationTime = newSupport.CreationTime.GetShamsi();
            output.Support = support != null
                ? ObjectMapper.Map<SupportCreateOrUpdateInput>(support)
                : newSupport;
            
            //FirmTypes
            output.SupportTypes = _supportTypeRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ))
                .ToList();

           output.SupportStates = _supportStateRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
           
           return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Administration_Support_Create, AppPermissions.Pages_Administration_Support_Edit)]
        public async Task CreateOrUpdateSupport(SupportCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateSupportAsync(input);
            }
            else
            {
                await CreateSupportAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_Administration_Support_Delete)]
        public async Task DeleteSupport(EntityDto input)
        {
            try
            {
                await _supportRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Support_Edit)]
        private async Task UpdateSupportAsync(SupportCreateOrUpdateInput input)
        {
            var support = ObjectMapper.Map<Support>(input);
            await _supportRepository.UpdateAsync(support);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Administration_Support_Create)]
        private async Task CreateSupportAsync(SupportCreateOrUpdateInput input)
        {
            var support = ObjectMapper.Map<Support>(input);
            support.UserId = AbpSession.UserId;
            await _supportRepository.InsertAsync(support);
        }
        
        private IQueryable<Support> GetFilteredQuery(GetSupportInput input)
        {
            
            var query = QueryableExtensions.WhereIf(
                _supportRepository.GetAll()
                .Include(x => x.SupportType)
                .Include(x => x.SupportState),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Description.Contains(input.Filter) ||
                    u.SupportType.Name.Contains(input.Filter) ||
                    u.SupportState.Name.Contains(input.Filter) ||
                    u.Response.Contains(input.Filter));
                                
            return query;
        }
    }
}