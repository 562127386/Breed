using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Contractors.Dto
{
    public class GetContractorForEditOutput
    {
        public ContractorCreateOrUpdateInput Contractor { get; set; }
        
        public List<ComboboxItemDto> FirmTypes { get; set; }
        public List<ComboboxItemDto> StateInfos { get; set; }
        public List<ComboboxItemDto> CityInfos { get; set; }
        public List<ComboboxItemDto> RegionInfos { get; set; }
        public List<ComboboxItemDto> VillageInfos { get; set; }
        public List<ComboboxItemDto> UnionInfos { get; set; }
        
        public GetContractorForEditOutput()
        {
            FirmTypes = new List<ComboboxItemDto>();
            StateInfos = new List<ComboboxItemDto>();
            CityInfos = new List<ComboboxItemDto>();
            RegionInfos = new List<ComboboxItemDto>();
            VillageInfos = new List<ComboboxItemDto>();
            UnionInfos = new List<ComboboxItemDto>();
        }
    }
}