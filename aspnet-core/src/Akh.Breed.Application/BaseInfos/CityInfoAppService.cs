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
using Akh.Breed.Contractors.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class CityInfoAppService :  BreedAppServiceBase, ICityInfoAppService
    {
        private readonly IRepository<CityInfo> _cityInfoRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;

        public CityInfoAppService(IRepository<CityInfo> cityInfoRepository, IRepository<StateInfo> stateInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
            _stateInfoRepository = stateInfoRepository;
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
        
        public async Task<CityInfoGetForEditOutput> GetCityInfoForEdit(NullableIdDto<int> input)
        {
            CityInfo cityInfo = null;
            if (input.Id.HasValue)
            {
                cityInfo = await _cityInfoRepository.GetAsync(input.Id.Value);
            }

            //Getting all available roles
            var output = new CityInfoGetForEditOutput();
            
            //cityInfo
            output.CityInfo = cityInfo != null
                ? ObjectMapper.Map<CityInfoCreateOrUpdateInput>(cityInfo)
                : new CityInfoCreateOrUpdateInput();
            
            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name)
                    {IsSelected = output.CityInfo.StateInfoId.Equals(c.Id)})
                .ToList();
                
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
            var query = QueryableExtensions.WhereIf(_cityInfoRepository.GetAllIncluding(p => p.StateInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.StateInfo.Name.Contains(input.Filter) ||
                    u.StateInfo.Code.Contains(input.Filter) ||
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
    }
}