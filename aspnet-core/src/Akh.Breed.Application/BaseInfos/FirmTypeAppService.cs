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
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class FirmTypeAppService :  BreedAppServiceBase, IFirmTypeAppService
    {
        private readonly IRepository<FirmType> _firmTypeRepository;

        public FirmTypeAppService(IRepository<FirmType> firmTypeRepository)
        {
            _firmTypeRepository = firmTypeRepository;
        }

        public async Task<PagedResultDto<FirmTypeListDto>> GetFirmType(GetFirmTypeInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var firmTypes = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var firmTypesListDto = ObjectMapper.Map<List<FirmTypeListDto>>(firmTypes);
            return new PagedResultDto<FirmTypeListDto>(
                userCount,
                firmTypesListDto
            );
        }
        
        public async Task<FirmTypeCreateOrUpdateInput> GetFirmTypeForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new FirmTypeCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var firmType = await _firmTypeRepository.GetAsync(input.Id.Value);
                if (firmType != null)
                    ObjectMapper.Map<FirmType,FirmTypeCreateOrUpdateInput>(firmType,output);
            }

            return output;
        }
        
        public async Task CreateOrUpdateFirmType(FirmTypeCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateFirmTypeAsync(input);
            }
            else
            {
                await CreateFirmTypeAsync(input);
            }
        }
        
        public async Task DeleteFirmType(EntityDto input)
        {
            try
            {
                await _firmTypeRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        private async Task UpdateFirmTypeAsync(FirmTypeCreateOrUpdateInput input)
        {
            var firmType = ObjectMapper.Map<FirmType>(input);
            await _firmTypeRepository.UpdateAsync(firmType);
        }
        
        private async Task CreateFirmTypeAsync(FirmTypeCreateOrUpdateInput input)
        {
            var firmType = ObjectMapper.Map<FirmType>(input);
            await _firmTypeRepository.InsertAsync(firmType);
        }
        
        private IQueryable<FirmType> GetFilteredQuery(GetFirmTypeInput input)
        {
            var query = QueryableExtensions.WhereIf(_firmTypeRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(FirmTypeCreateOrUpdateInput input)
        {
            var existingObj = (await _firmTypeRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _firmTypeRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}