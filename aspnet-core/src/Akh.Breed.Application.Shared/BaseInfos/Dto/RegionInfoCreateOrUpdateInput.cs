namespace Akh.Breed.BaseInfos.Dto
{
    public class RegionInfoCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string Name { get; set; }

        public string Code { get; set; }

        public int? StateInfoId { get; set; }
        
        public int? CityInfoId { get; set; }
    }
}