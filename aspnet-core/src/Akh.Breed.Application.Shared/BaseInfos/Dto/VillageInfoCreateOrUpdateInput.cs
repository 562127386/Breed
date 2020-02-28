namespace Akh.Breed.BaseInfos.Dto
{
    public class VillageInfoCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string Name { get; set; }

        public string Code { get; set; }

        public int? RegionInfoId { get; set; }
        
        public int? CityInfoId { get; set; }
        public int? StateInfoId { get; set; }
    }
}