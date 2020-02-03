using Abp.Application.Services.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class GetProviderInfoInput
    {
        public string Filter { get; set; }
    }
    
    public class ProviderInfoListDto : EntityDto
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}