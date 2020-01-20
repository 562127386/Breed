using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Akh.Breed.Web.Areas.AppAreaName.Models.Layout;
using Akh.Breed.Web.Session;
using Akh.Breed.Web.Views;

namespace Akh.Breed.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameTheme7Footer
{
    public class AppAreaNameTheme7FooterViewComponent : BreedViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameTheme7FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}

