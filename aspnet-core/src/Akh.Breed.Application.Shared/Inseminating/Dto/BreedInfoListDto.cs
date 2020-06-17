using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Inseminating.Dto
{
    public class GetBreedInfoInput : PagedAndSortedInputDto, IShouldNormalize
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
    
    public class BreedInfoListDto : EntityDto
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}