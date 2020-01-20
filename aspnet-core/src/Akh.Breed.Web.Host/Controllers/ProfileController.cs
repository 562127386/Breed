using Abp.AspNetCore.Mvc.Authorization;
using Akh.Breed.Storage;

namespace Akh.Breed.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(ITempFileCacheManager tempFileCacheManager) :
            base(tempFileCacheManager)
        {
        }
    }
}
