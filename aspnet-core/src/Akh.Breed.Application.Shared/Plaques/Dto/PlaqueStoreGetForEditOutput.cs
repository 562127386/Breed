using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueStoreGetForEditOutput
    {
        public PlaqueStoreCreateOrUpdateInput PlaqueStore { get; set; }
        
        public List<ComboboxItemDto> SpecieInfos { get; set; }

        public PlaqueStoreGetForEditOutput()
        {
            SpecieInfos = new List<ComboboxItemDto>();
        }
    }
}