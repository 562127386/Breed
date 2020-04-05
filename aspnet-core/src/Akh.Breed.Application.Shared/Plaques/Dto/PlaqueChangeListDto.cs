using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class GetPlaqueChangeInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "PlaqueId";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class PlaqueChangeListDto : EntityDto
    {
        public string PlaqueCode { get; set; }
        
        public string PlaqueHerdName { get; set; }
        
        public string ChangeReason { get; set; }

        public string PreStateName { get; set; }
        
        public string NewStateName { get; set; }
        
        public string OfficerName { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        
        
    }
}