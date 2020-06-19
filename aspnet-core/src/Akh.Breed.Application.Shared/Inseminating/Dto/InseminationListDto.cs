using System;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Inseminating.Dto
{
    public class GetInseminationInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
            Sorting = Sorting.Replace("speciesInfoName", "SpeciesInfo.Name");
            Sorting = Sorting.Replace("herdName", "Herd.HerdName");
            Sorting = Sorting.Replace("activityInfoName", "ActivityInfo.Name");
            Sorting = Sorting.Replace("officerName", "Officer.Family");

            Filter = Filter?.Trim();
        }
    }
    public class InseminationListDto : EntityDto
    {
        public string NationalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public string BirthDateStr { get; set; }
        public string CreationTimeStr { get; set; }
        
        public string SpeciesInfoName { get; set; }

        public string SexInfoName { get; set; }

        public string HerdName { get; set; }

        public string ActivityInfoName { get; set; }

        public string OfficerName { get; set; }
        
        
        public DateTime CreationTime { get; set; }
        
    }
}