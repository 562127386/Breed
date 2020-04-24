using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueToCityGetForEditOutput
    {
        public PlaqueToCityCreateOrUpdateInput PlaqueToCity { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }

        public List<ComboboxItemDto> CityInfos { get; set; }
        
        public List<ComboboxItemDto> SpeciesInfos { get; set; }

        public PlaqueToCityGetForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
            CityInfos = new List<ComboboxItemDto>();
            SpeciesInfos = new List<ComboboxItemDto>();
        }
    }
}