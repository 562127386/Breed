using System;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class GetPlaqueToHerdInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "NationalCode";
            }

            Filter = Filter?.Trim();
        }
    }
    public class PlaqueToHerdListDto : EntityDto
    {
        public string NationalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public string HerdName { get; set; }
        public string OfficerName { get; set; }
        
        public DateTime CreationTime { get; set; }
        
    }
}