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
    public class CityInfoAppService :  BreedAppServiceBase, ICityInfoAppService
    {
        private readonly IRepository<CityInfo> _cityInfoRepository;

        public CityInfoAppService(IRepository<CityInfo> cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        public async Task<PagedResultDto<CityInfoListDto>> GetCityInfo(GetCityInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var cityInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var cityInfosListDto = ObjectMapper.Map<List<CityInfoListDto>>(cityInfos);
            return new PagedResultDto<CityInfoListDto>(
                userCount,
                cityInfosListDto
            );
        }
        
        public async Task<CityInfoCreateOrUpdateInput> GetCityInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new CityInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var cityInfo = await _cityInfoRepository.GetAsync(input.Id.Value);
                if (cityInfo != null)
                    ObjectMapper.Map<CityInfo,CityInfoCreateOrUpdateInput>(cityInfo,output);
            }

            return output;
        }
        
        public async Task CreateOrUpdateCityInfo(CityInfoCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateCityInfoAsync(input);
            }
            else
            {
                await CreateCityInfoAsync(input);
            }
        }
        
        public async Task DeleteCityInfo(EntityDto input)
        {
            await _cityInfoRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateCityInfoAsync(CityInfoCreateOrUpdateInput input)
        {
            var cityInfo = ObjectMapper.Map<CityInfo>(input);
            await _cityInfoRepository.UpdateAsync(cityInfo);
        }
        
        private async Task CreateCityInfoAsync(CityInfoCreateOrUpdateInput input)
        {
            var cityInfo = ObjectMapper.Map<CityInfo>(input);
            await _cityInfoRepository.InsertAsync(cityInfo);
        }
        
        private IQueryable<CityInfo> GetFilteredQuery(GetCityInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_cityInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
    }
}