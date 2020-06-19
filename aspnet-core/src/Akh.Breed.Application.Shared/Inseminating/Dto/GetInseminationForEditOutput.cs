using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Inseminating.Dto
{
    public class GetInseminationForEditOutput
    {
        public InseminationCreateOrUpdateInput Insemination { get; set; }
        
        public List<ComboboxItemDto> SpeciesInfos { get; set; }
        public List<ComboboxItemDto> SexInfos { get; set; }
        public List<ComboboxItemDto> Herds { get; set; }
        public List<ComboboxItemDto> ActivityInfos { get; set; }
        public List<ComboboxItemDto> BreedInfos { get; set; }
        public List<ComboboxItemDto> BirthTypeInfos { get; set; }
        public List<ComboboxItemDto> AnomalyInfos { get; set; }
        public List<ComboboxItemDto> MembershipInfos { get; set; }
        public List<ComboboxItemDto> BodyColorInfos { get; set; }
        public List<ComboboxItemDto> SpotConnectorInfos { get; set; }
        
        public GetInseminationForEditOutput()
        {
            SpeciesInfos = new List<ComboboxItemDto>();
            SexInfos = new List<ComboboxItemDto>();
            Herds = new List<ComboboxItemDto>();
            ActivityInfos = new List<ComboboxItemDto>();
            BreedInfos = new List<ComboboxItemDto>();
            BirthTypeInfos = new List<ComboboxItemDto>();
            AnomalyInfos = new List<ComboboxItemDto>();
            MembershipInfos = new List<ComboboxItemDto>();
            BodyColorInfos = new List<ComboboxItemDto>();
            SpotConnectorInfos = new List<ComboboxItemDto>();
        }
    }
}