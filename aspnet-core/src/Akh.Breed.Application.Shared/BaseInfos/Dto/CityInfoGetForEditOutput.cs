using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class CityInfoGetForEditOutput
    {
        public CityInfoCreateOrUpdateInput CityInfo { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }

        public CityInfoGetForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
        }
    }
}