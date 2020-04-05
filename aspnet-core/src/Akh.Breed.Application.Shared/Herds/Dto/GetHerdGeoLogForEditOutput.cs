using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Herds.Dto
{
    public class GetHerdGeoLogForEditOutput
    {
        public HerdGeoLogCreateOrUpdateInput HerdGeoLog { get; set; }
        
        public List<ComboboxItemDto> Herds { get; set; }

        public GetHerdGeoLogForEditOutput()
        {
            Herds = new List<ComboboxItemDto>();
        }
    }
}