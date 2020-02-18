using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.Contractors.Dto
{
    public class GetContractorForEditOutput
    {
        public ContractorEditDto Contractor { get; set; }
        
        public List<ComboboxItemDto> FirmTypes { get; set; }
        
        public GetContractorForEditOutput()
        {
            FirmTypes = new List<ComboboxItemDto>();
        }
    }
}