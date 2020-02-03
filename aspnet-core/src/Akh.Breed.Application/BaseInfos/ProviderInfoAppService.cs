using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public class ProviderInfoAppService :  BreedAppServiceBase, IProviderInfoAppService
    {
        private readonly IRepository<ProviderInfo> _providerInfoRepository;

        public ProviderInfoAppService(IRepository<ProviderInfo> providerInfoRepository)
        {
            _providerInfoRepository = providerInfoRepository;
        }

        public ListResultDto<ProviderInfoListDto> GetProviderInfo(GetProviderInfoInput input)
        {
            var providerInfo = _providerInfoRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Name.Contains(input.Filter) ||
                         p.Code.Contains(input.Filter) 
                )
                .OrderBy(p => p.Name)
                .ToList();

            return new ListResultDto<ProviderInfoListDto>(ObjectMapper.Map<List<ProviderInfoListDto>>(providerInfo));
        }
        
        public async Task CreateProviderInfo(ProviderInfoCreateInput input)
        {
            var providerInfo = ObjectMapper.Map<ProviderInfo>(input);
            await _providerInfoRepository.InsertAsync(providerInfo);
        }
        
        public async Task DeleteProviderInfo(EntityDto input)
        {
            await _providerInfoRepository.DeleteAsync(input.Id);
        }
    }
}