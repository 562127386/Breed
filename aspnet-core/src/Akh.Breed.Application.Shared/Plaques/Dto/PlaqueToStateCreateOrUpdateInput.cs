using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Timing;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueToStateCreateOrUpdateInput
    {
        public const int CodeLength = 15; 
        
        public int? Id { get; set; }
        
        public int PlaqueCount { get; set; }

        public long FromCode { get; set; }
        
        public long ToCode { get; set; }
        
        public int? StateInfoId { get; set; }
        
        public int? PlaqueStoreId { get; set; }
        public int? SpeciesInfoId { get; set; }

        public long? FinishedPlaqueId { get; set; }
        
        public DateTime? SetTime { get; set; }

        public PlaqueToStateCreateOrUpdateInput()
        {
            SetTime = Clock.Now;
        } 
    }
}