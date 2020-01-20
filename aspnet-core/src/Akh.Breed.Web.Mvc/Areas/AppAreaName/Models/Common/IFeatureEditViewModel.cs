using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Akh.Breed.Editions.Dto;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}
