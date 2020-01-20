using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Authorization.Permissions.Dto;

namespace Akh.Breed.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}

