using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class SexInfoAppService :  BreedAppServiceBase, ISexInfoAppService
    {
        private readonly IRepository<SexInfo> _sexInfoRepository;

        public SexInfoAppService(IRepository<SexInfo> sexInfoRepository)
        {
            _sexInfoRepository = sexInfoRepository;
        }

        public async Task<PagedResultDto<SexInfoListDto>> GetSexInfo(GetSexInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var sexInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var sexInfosListDto = ObjectMapper.Map<List<SexInfoListDto>>(sexInfos);
            return new PagedResultDto<SexInfoListDto>(
                userCount,
                sexInfosListDto
            );
        }
        
        public async Task<SexInfoCreateOrUpdateInput> GetSexInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new SexInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var sexInfo = await _sexInfoRepository.GetAsync(input.Id.Value);
                if (sexInfo != null)
                    ObjectMapper.Map<SexInfo,SexInfoCreateOrUpdateInput>(sexInfo,output);
            }

            return output;
        }
        
        public async Task CreateOrUpdateSexInfo(SexInfoCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateSexInfoAsync(input);
            }
            else
            {
                await CreateSexInfoAsync(input);
            }
        }
        
        public async Task DeleteSexInfo(EntityDto input)
        {
            await _sexInfoRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateSexInfoAsync(SexInfoCreateOrUpdateInput input)
        {
            var sexInfo = ObjectMapper.Map<SexInfo>(input);
            await _sexInfoRepository.UpdateAsync(sexInfo);
        }
        
        private async Task CreateSexInfoAsync(SexInfoCreateOrUpdateInput input)
        {
            var sexInfo = ObjectMapper.Map<SexInfo>(input);
            await _sexInfoRepository.InsertAsync(sexInfo);
        }
        
        private IQueryable<SexInfo> GetFilteredQuery(GetSexInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_sexInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
    }
}