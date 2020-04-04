using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueChangeGetForEditOutput
    {
        public PlaqueChangeCreateOrUpdateInput PlaqueChange { get; set; }
        
        public List<ComboboxItemDto> PlaqueStates { get; set; }

        public PlaqueChangeGetForEditOutput()
        {
            PlaqueStates = new List<ComboboxItemDto>();
        }
    }
}