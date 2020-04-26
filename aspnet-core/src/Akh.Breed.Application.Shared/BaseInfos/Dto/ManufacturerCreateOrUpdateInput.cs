namespace Akh.Breed.BaseInfos.Dto
{
    public class ManufacturerCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsImporter { get; set; }
    }
}