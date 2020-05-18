using System;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Livestocks.Dto
{
    public class GetMonitoringInput : PagedAndSortedInputDto, IShouldNormalize
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
    public class MonitoringListDto : EntityDto
    {
        public string NationalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public string SpeciesInfoName { get; set; }

        public string HerdName { get; set; }
        
        public string HerdCode { get; set; }
        
        public string HerdOwner { get; set; }

        public string OfficerName { get; set; }
        
        public string ContractorName { get; set; }
        
        public string ContractorCode { get; set; }
        
        
        public DateTime CreationTime { get; set; }
        
    }
}