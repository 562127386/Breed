using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Inseminating.Dto
{
    public class GetBodyColorInfoInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class BodyColorInfoListDto : EntityDto
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}