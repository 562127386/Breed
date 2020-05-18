using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Support.Dto
{
    public class SupportGetForEditOutput
    {
        public SupportCreateOrUpdateInput Support { get; set; }
        
        public List<ComboboxItemDto> SupportTypes { get; set; }
        
        public List<ComboboxItemDto> SupportStates { get; set; }

        public SupportGetForEditOutput()
        {
            SupportTypes = new List<ComboboxItemDto>();
            SupportStates = new List<ComboboxItemDto>();
        }
    }
}