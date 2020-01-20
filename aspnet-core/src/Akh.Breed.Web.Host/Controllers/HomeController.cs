using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace Akh.Breed.Web.Controllers
{
    public class HomeController : BreedControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Ui");
        }
    }
}

