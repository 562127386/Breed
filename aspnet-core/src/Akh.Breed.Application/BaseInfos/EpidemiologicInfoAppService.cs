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
    public class EpidemiologicInfoAppService :  BreedAppServiceBase, IEpidemiologicInfoAppService
    {
        private readonly IRepository<EpidemiologicInfo> _epidemiologicInfoRepository;

        public EpidemiologicInfoAppService(IRepository<EpidemiologicInfo> epidemiologicInfoRepository)
        {
            _epidemiologicInfoRepository = epidemiologicInfoRepository;
        }

        public async Task<PagedResultDto<EpidemiologicInfoListDto>> GetEpidemiologicInfo(GetEpidemiologicInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var epidemiologicInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var epidemiologicInfosListDto = ObjectMapper.Map<List<EpidemiologicInfoListDto>>(epidemiologicInfos);
            return new PagedResultDto<EpidemiologicInfoListDto>(
                userCount,
                epidemiologicInfosListDto
            );
        }
        
        public async Task<EpidemiologicInfoCreateOrUpdateInput> GetEpidemiologicInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new EpidemiologicInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var epidemiologicInfo = await _epidemiologicInfoRepository.GetAsync(input.Id.Value);
                if (epidemiologicInfo != null)
                    ObjectMapper.Map<EpidemiologicInfo,EpidemiologicInfoCreateOrUpdateInput>(epidemiologicInfo,output);
            }

            return output;
        }
        
        public async Task CreateOrUpdateEpidemiologicInfo(EpidemiologicInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateEpidemiologicInfoAsync(input);
            }
            else
            {
                await CreateEpidemiologicInfoAsync(input);
            }
        }
        
        public async Task DeleteEpidemiologicInfo(EntityDto input)
        {
            await _epidemiologicInfoRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateEpidemiologicInfoAsync(EpidemiologicInfoCreateOrUpdateInput input)
        {
            var epidemiologicInfo = ObjectMapper.Map<EpidemiologicInfo>(input);
            await _epidemiologicInfoRepository.UpdateAsync(epidemiologicInfo);
        }
        
        private async Task CreateEpidemiologicInfoAsync(EpidemiologicInfoCreateOrUpdateInput input)
        {
            var epidemiologicInfo = ObjectMapper.Map<EpidemiologicInfo>(input);
            await _epidemiologicInfoRepository.InsertAsync(epidemiologicInfo);
        }
        
        private IQueryable<EpidemiologicInfo> GetFilteredQuery(GetEpidemiologicInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_epidemiologicInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Family.Contains(input.Filter) ||
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(EpidemiologicInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _epidemiologicInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
        }
    }
}