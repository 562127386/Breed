using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Akh.Breed.Web.Areas.AppAreaName.Models.Layout;
using Akh.Breed.Web.Session;
using Akh.Breed.Web.Views;

namespace Akh.Breed.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameTheme3Brand
{
    public class AppAreaNameTheme3BrandViewComponent : BreedViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameTheme3BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}

