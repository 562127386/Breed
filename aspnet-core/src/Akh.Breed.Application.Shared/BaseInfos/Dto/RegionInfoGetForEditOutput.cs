using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class RegionInfoGetForEditOutput
    {
        public RegionInfoCreateOrUpdateInput RegionInfo { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }
        public List<ComboboxItemDto> CityInfos { get; set; }

        public RegionInfoGetForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
            CityInfos = new List<ComboboxItemDto>();
        }
    }
}