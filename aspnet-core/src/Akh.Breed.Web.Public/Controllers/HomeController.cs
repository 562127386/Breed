using Microsoft.AspNetCore.Mvc;
using Akh.Breed.Web.Controllers;

namespace Akh.Breed.Web.Public.Controllers
{
    public class HomeController : BreedControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
