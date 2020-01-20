using Microsoft.AspNetCore.Antiforgery;

namespace Akh.Breed.Web.Controllers
{
    public class AntiForgeryController : BreedControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}

