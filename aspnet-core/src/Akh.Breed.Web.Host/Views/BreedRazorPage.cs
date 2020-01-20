using Abp.AspNetCore.Mvc.Views;

namespace Akh.Breed.Web.Views
{
    public abstract class BreedRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected BreedRazorPage()
        {
            LocalizationSourceName = BreedConsts.LocalizationSourceName;
        }
    }
}

