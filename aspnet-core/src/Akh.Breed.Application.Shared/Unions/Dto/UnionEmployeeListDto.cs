using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Unions.Dto
{
    public class GetUnionEmployeeInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public int? UnionInfoId { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime Desc";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class UnionEmployeeListDto : EntityDto
    {

        public string NationalCode { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Phone { get; set; }

        public string Post { get; set; }

    }
}