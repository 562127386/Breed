using System;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Support.Dto
{
    public class GetSupportInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
            Sorting = Sorting.Replace("supportTypeName", "SupportType.Name");
            Sorting = Sorting.Replace("supportStateName", "SupportState.Name");
            Filter = Filter?.Trim();
        }
    }
    
    public class SupportListDto : EntityDto
    {
        public string Description { get; set; }
        
        public string Response { get; set; }
        
        public string SupportTypeName { get; set; }
        
        public string SupportStateName { get; set; }
        
        public string UserName { get; set; }
        
        public DateTime CreationTime { get; set; }
    }
}