using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class GetEpidemiologicInfoInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Family,CreationTime";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class EpidemiologicInfoListDto : EntityDto
    {
        public string Name { get; set; }
        
        public string Family { get; set; }

        public string Code { get; set; }
    }
}