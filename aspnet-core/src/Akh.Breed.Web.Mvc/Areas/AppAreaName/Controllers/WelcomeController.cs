using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Akh.Breed.Web.Controllers;

namespace Akh.Breed.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize]
    public class WelcomeController : BreedControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
