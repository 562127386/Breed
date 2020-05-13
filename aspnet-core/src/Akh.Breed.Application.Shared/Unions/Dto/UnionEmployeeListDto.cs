using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Unions.Dto
{
    public class GetUnionInfoInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime Desc";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class UnionInfoListDto : EntityDto
    {
        public string UnionName { get; set; }

        public string Code { get; set; }

        public string NationalCode { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public int StateInfoId { get; set; }
        
        public string StateInfoName { get; set; }
    }
}