using Abp.AspNetCore.Mvc.ViewComponents;

namespace Akh.Breed.Web.Views
{
    public abstract class BreedViewComponent : AbpViewComponent
    {
        protected BreedViewComponent()
        {
            LocalizationSourceName = BreedConsts.LocalizationSourceName;
        }
    }
}
