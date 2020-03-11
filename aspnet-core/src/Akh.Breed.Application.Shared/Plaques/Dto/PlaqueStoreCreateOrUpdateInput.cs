using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueStoreCreateOrUpdateInput
    {
        public const int CodeLength = 15; 
        
        public int? Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }
        
        [Required]
        public int? RegionInfoId { get; set; }
        
        public int? CityInfoId { get; set; }
        public int? StateInfoId { get; set; }
        
        [Required]
        [StringLength(CodeLength)]
        public string FromCode { get; set; }
        
        [Required]
        [StringLength(CodeLength)]
        public string ToCode { get; set; }
        
        public int? SpeciesId { get; set; }

        public long? FinishedPlaqueId { get; set; }
    }
}