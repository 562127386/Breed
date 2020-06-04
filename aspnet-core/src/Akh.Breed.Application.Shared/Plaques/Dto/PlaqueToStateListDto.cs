using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class GetPlaqueToStateInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
            Sorting = Sorting.Replace("stateInfoName", "StateInfo.Name");
            Sorting = Sorting.Replace("speciesName", "PlaqueStore.Species.Name");

            Filter = Filter?.Trim();
        }
    }
    
    public class PlaqueToStateListDto : EntityDto
    {
        public long FromCode { get; set; }
        
        public long ToCode { get; set; }
        public int PlaqueCount { get; set; }
        
        public long PlaqueAllocated { get; set; }

        public int PlaqueUsed { get; set; }

        public DateTime SetTime { get; set; }
        
        public string StateName { get; set; }
        
        public string SpeciesName { get; set; }
        
    }
}