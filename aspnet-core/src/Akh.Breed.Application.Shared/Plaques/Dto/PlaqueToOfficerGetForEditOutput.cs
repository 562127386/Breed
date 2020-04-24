using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueToOfficerGetForEditOutput
    {
        public PlaqueToOfficerCreateOrUpdateInput PlaqueToOfficer { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }
        
        public List<ComboboxItemDto> CityInfos { get; set; }
        
        public List<ComboboxItemDto> OfficerInfos { get; set; }
        public List<ComboboxItemDto> SpeciesInfos { get; set; }

        public PlaqueToOfficerGetForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
            CityInfos = new List<ComboboxItemDto>();
            OfficerInfos = new List<ComboboxItemDto>();
            SpeciesInfos = new List<ComboboxItemDto>();
        }
    }
}