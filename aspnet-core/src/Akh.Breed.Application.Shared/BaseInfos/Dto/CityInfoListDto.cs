using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class GetCityInfoInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Code,Name";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class CityInfoListDto : EntityDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int StateInfoId { get; set; }
    }
}