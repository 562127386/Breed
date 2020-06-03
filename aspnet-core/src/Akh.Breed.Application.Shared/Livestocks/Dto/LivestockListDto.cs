using System;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Livestocks.Dto
{
    public class GetLivestockInput : PagedAndSortedInputDto, IShouldNormalize
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
    public class LivestockListDto : EntityDto
    {
        public string NationalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public bool Imported { get; set; }

        public string BirthDateStr { get; set; }
        
        public string SpeciesInfoName { get; set; }

        public string SexInfoName { get; set; }

        public string HerdName { get; set; }

        public string ActivityInfoName { get; set; }

        public string OfficerName { get; set; }
        
        
        public DateTime CreationTime { get; set; }
        
    }
}