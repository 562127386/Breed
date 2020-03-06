using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.BaseInfos.Dto
{
    public class VillageInfoCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }
        
        [Required]
        public int? RegionInfoId { get; set; }
        
        public int? CityInfoId { get; set; }
        public int? StateInfoId { get; set; }
    }
}