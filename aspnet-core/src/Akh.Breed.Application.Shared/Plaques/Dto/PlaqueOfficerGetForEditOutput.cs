using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueOfficerGetForEditOutput
    {
        public PlaqueOfficerCreateOrUpdateInput PlaqueOfficer { get; set; }
        
        public List<ComboboxItemDto> Officers { get; set; }
        public List<ComboboxItemDto> SpeciesInfos { get; set; }

        public PlaqueOfficerGetForEditOutput()
        {
            Officers = new List<ComboboxItemDto>();
            SpeciesInfos = new List<ComboboxItemDto>();
        }
    }
}