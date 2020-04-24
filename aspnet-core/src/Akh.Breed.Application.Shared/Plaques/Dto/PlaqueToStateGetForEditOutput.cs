using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueToStateGetForEditOutput
    {
        public PlaqueToStateCreateOrUpdateInput PlaqueToState { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }
        public List<ComboboxItemDto> SpeciesInfos { get; set; }

        public PlaqueToStateGetForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
            SpeciesInfos = new List<ComboboxItemDto>();
        }
    }
}