using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Akh.Breed.Editions.Dto;
using Akh.Breed.Web.Areas.AppAreaName.Models.Common;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.Editions
{
    [AutoMapFrom(typeof(GetEditionEditOutput))]
    public class CreateEditionModalViewModel : GetEditionEditOutput, IFeatureEditViewModel
    {
        public IReadOnlyList<ComboboxItemDto> EditionItems { get; set; }

        public IReadOnlyList<ComboboxItemDto> FreeEditionItems { get; set; }
    }
}
