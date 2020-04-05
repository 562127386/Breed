using System;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Herds.Dto
{
    public class GetHerdGeoLogInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "HerdId";
            }

            Filter = Filter?.Trim();
        }
    }
    public class HerdGeoLogListDto : EntityDto
    {
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }
        
        public string HerdName { get; set; }
        
        public string OfficerName { get; set; }

        public DateTime CreationTime { get; set; }
        
    }
}