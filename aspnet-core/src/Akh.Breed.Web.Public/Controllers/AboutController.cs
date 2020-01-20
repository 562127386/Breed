using Microsoft.AspNetCore.Mvc;
using Akh.Breed.Web.Controllers;

namespace Akh.Breed.Web.Public.Controllers
{
    public class AboutController : BreedControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
