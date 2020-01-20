using Abp.AspNetCore.Mvc.Authorization;
using Akh.Breed.Authorization;
using Akh.Breed.Storage;
using Abp.BackgroundJobs;
using Abp.Authorization;

namespace Akh.Breed.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}
