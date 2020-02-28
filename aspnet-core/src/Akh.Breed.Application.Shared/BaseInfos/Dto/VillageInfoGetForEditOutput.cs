using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class VillageInfoGetForEditOutput
    {
        public VillageInfoCreateOrUpdateInput VillageInfo { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }
        public List<ComboboxItemDto> CityInfos { get; set; }
        
        public List<ComboboxItemDto> RegionInfos { get; set; }

        public VillageInfoGetForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
            CityInfos = new List<ComboboxItemDto>();
            RegionInfos = new List<ComboboxItemDto>();
        }
    }
}