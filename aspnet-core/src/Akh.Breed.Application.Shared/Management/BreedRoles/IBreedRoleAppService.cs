﻿using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Management.BreedRoles.Dto;

namespace Akh.Breed.Management.BreedRoles
{
    /// <summary>
    /// Application service that is used by 'role management' page.
    /// </summary>
    public interface IBreedRoleAppService : IApplicationService
    {
        Task<ListResultDto<BreedRoleListDto>> GetBreedRoles(GetBreedRolesInput input);

        Task<GetBreedRoleForEditOutput> GetBreedRoleForEdit(NullableIdDto input);

        Task CreateOrUpdateBreedRole(CreateOrUpdateBreedRoleInput input);

        Task DeleteBreedRole(EntityDto input);
    }
}
