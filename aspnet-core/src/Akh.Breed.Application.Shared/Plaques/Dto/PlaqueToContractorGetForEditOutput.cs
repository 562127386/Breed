using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueToContractorGetForEditOutput
    {
        public PlaqueToContractorCreateOrUpdateInput PlaqueToContractor { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }

        public List<ComboboxItemDto> Contractors { get; set; }
        
        public List<ComboboxItemDto> SpeciesInfos { get; set; }

        public PlaqueToContractorGetForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
            Contractors = new List<ComboboxItemDto>();
            SpeciesInfos = new List<ComboboxItemDto>();
        }
    }
}