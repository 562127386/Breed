using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Akh.Breed.Web.Public.Views
{
    public abstract class BreedRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected BreedRazorPage()
        {
            LocalizationSourceName = BreedConsts.LocalizationSourceName;
        }
    }
}

