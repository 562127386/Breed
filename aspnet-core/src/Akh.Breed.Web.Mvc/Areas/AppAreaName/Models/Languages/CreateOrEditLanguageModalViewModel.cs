using Abp.AutoMapper;
using Akh.Breed.Localization.Dto;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.Languages
{
    [AutoMapFrom(typeof(GetLanguageForEditOutput))]
    public class CreateOrEditLanguageModalViewModel : GetLanguageForEditOutput
    {
        public bool IsEditMode => Language.Id.HasValue;
    }
}
