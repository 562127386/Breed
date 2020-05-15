using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueToOfficerGetForEditOutput
    {
        public PlaqueToOfficerCreateOrUpdateInput PlaqueToOfficer { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }
        
        public List<ComboboxItemDto> Contractors { get; set; }
        
        public List<ComboboxItemDto> Officers { get; set; }
        public List<ComboboxItemDto> SpeciesInfos { get; set; }

        public PlaqueToOfficerGetForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
            Contractors = new List<ComboboxItemDto>();
            Officers = new List<ComboboxItemDto>();
            SpeciesInfos = new List<ComboboxItemDto>();
        }
    }
}