using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Officers.Dto
{
    public class GetOfficerForEditOutput
    {
        public OfficerCreateOrUpdateInput Officer { get; set; }
        
        public List<ComboboxItemDto> AcademicDegrees { get; set; }
        public List<ComboboxItemDto> StateInfos { get; set; }
        public List<ComboboxItemDto> Contractors { get; set; }
        
        public GetOfficerForEditOutput()
        {
            AcademicDegrees = new List<ComboboxItemDto>();
            StateInfos = new List<ComboboxItemDto>();
            Contractors = new List<ComboboxItemDto>();
        }
    }
}