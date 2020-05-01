using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Timing;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueStoreCreateOrUpdateInput
    {
        public const int CodeLength = 15; 
        
        public int? Id { get; set; }
       
        [Required]
        public long FromCode { get; set; }
        
        [Required]
        public long ToCode { get; set; }
        
        public DateTime SetTime { get; set; }
        
        public int? SpeciesId { get; set; }
        
        public int? ManufacturerId { get; set; }

        public long? FinishedPlaqueId { get; set; }

        public PlaqueStoreCreateOrUpdateInput()
        {
            SetTime = Clock.Now;
        }
    }
}