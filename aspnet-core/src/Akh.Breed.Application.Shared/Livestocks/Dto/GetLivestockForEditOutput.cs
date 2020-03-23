using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Livestocks.Dto
{
    public class GetLivestockForEditOutput
    {
        public LivestockCreateOrUpdateInput Livestock { get; set; }
        
        public List<ComboboxItemDto> SpeciesInfos { get; set; }
        public List<ComboboxItemDto> SexInfos { get; set; }
        public List<ComboboxItemDto> Herds { get; set; }
        public List<ComboboxItemDto> ActivityInfos { get; set; }

        public GetLivestockForEditOutput()
        {
            SpeciesInfos = new List<ComboboxItemDto>();
            SexInfos = new List<ComboboxItemDto>();
            Herds = new List<ComboboxItemDto>();
            ActivityInfos = new List<ComboboxItemDto>();
        }
    }
}