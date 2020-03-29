using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Herds.Dto
{
    public class GetHerdForEditOutput
    {
        public HerdCreateOrUpdateInput Herd { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }
        public List<ComboboxItemDto> CityInfos { get; set; }
        public List<ComboboxItemDto> RegionInfos { get; set; }
        public List<ComboboxItemDto> VillageInfos { get; set; }
        public List<ComboboxItemDto> UnionInfos { get; set; }
        public List<ComboboxItemDto> ActivityInfos { get; set; }
        public List<ComboboxItemDto> Contractors { get; set; }

        public GetHerdForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
            CityInfos = new List<ComboboxItemDto>();
            RegionInfos = new List<ComboboxItemDto>();
            VillageInfos = new List<ComboboxItemDto>();
            UnionInfos = new List<ComboboxItemDto>();
            ActivityInfos = new List<ComboboxItemDto>();
            Contractors = new List<ComboboxItemDto>();
        }
    }
}