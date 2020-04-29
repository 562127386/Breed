using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueToHerdGetForEditOutput
    {
        public PlaqueToHerdCreateOrUpdateInput PlaqueToHerd { get; set; }
        
        public List<ComboboxItemDto> Herds { get; set; }
        public PlaqueToHerdGetForEditOutput()
        {
            Herds = new List<ComboboxItemDto>();
        }
    }
}