using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Akh.Breed.Editions.Dto;

namespace Akh.Breed.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}
