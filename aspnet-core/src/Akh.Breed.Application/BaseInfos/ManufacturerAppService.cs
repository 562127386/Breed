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
using Akh.Breed.Authorization;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class ManufacturerAppService :  BreedAppServiceBase, IManufacturerAppService
    {
        private readonly IRepository<Manufacturer> _manufacturerRepository;

        public ManufacturerAppService(IRepository<Manufacturer> manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_Manufacturer)]
        public async Task<PagedResultDto<ManufacturerListDto>> GetManufacturer(GetManufacturerInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var manufacturers = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var manufacturersListDto = ObjectMapper.Map<List<ManufacturerListDto>>(manufacturers);
            return new PagedResultDto<ManufacturerListDto>(
                userCount,
                manufacturersListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_Manufacturer_Create, AppPermissions.Pages_BaseInfo_Manufacturer_Edit)]
        public async Task<ManufacturerCreateOrUpdateInput> GetManufacturerForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new ManufacturerCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var manufacturer = await _manufacturerRepository.GetAsync(input.Id.Value);
                if (manufacturer != null)
                    ObjectMapper.Map<Manufacturer,ManufacturerCreateOrUpdateInput>(manufacturer,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_Manufacturer_Create, AppPermissions.Pages_BaseInfo_Manufacturer_Edit)]
        public async Task CreateOrUpdateManufacturer(ManufacturerCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateManufacturerAsync(input);
            }
            else
            {
                await CreateManufacturerAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_Manufacturer_Delete)]
        public async Task DeleteManufacturer(EntityDto input)
        {
            try
            {
                await _manufacturerRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_Manufacturer_Edit)]
        private async Task UpdateManufacturerAsync(ManufacturerCreateOrUpdateInput input)
        {
            var manufacturer = ObjectMapper.Map<Manufacturer>(input);
            await _manufacturerRepository.UpdateAsync(manufacturer);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_Manufacturer_Create)]
        private async Task CreateManufacturerAsync(ManufacturerCreateOrUpdateInput input)
        {
            var manufacturer = ObjectMapper.Map<Manufacturer>(input);
            await _manufacturerRepository.InsertAsync(manufacturer);
        }
        
        private IQueryable<Manufacturer> GetFilteredQuery(GetManufacturerInput input)
        {
            var query = QueryableExtensions.WhereIf(_manufacturerRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(ManufacturerCreateOrUpdateInput input)
        {
            var existingObj = (await _manufacturerRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _manufacturerRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}