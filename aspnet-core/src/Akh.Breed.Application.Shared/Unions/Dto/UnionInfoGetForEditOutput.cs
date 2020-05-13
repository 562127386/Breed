using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class UnionInfoGetForEditOutput
    {
        public UnionInfoCreateOrUpdateInput UnionInfo { get; set; }
        
        public List<ComboboxItemDto> StateInfos { get; set; }

        public UnionInfoGetForEditOutput()
        {
            StateInfos = new List<ComboboxItemDto>();
        }
    }
}