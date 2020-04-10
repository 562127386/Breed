using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.BaseInfos
{
    public class AcademicDegreeAppService :  BreedAppServiceBase, IAcademicDegreeAppService
    {
        private readonly IRepository<AcademicDegree> _academicDegreeRepository;

        public AcademicDegreeAppService(IRepository<AcademicDegree> academicDegreeRepository)
        {
            _academicDegreeRepository = academicDegreeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_AcademicDegree)]
        public async Task<PagedResultDto<AcademicDegreeListDto>> GetAcademicDegree(GetAcademicDegreeInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var academicDegrees = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var academicDegreesListDto = ObjectMapper.Map<List<AcademicDegreeListDto>>(academicDegrees);
            return new PagedResultDto<AcademicDegreeListDto>(
                userCount,
                academicDegreesListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_AcademicDegree_Create, AppPermissions.Pages_BaseInfo_AcademicDegree_Edit)]
        public async Task<AcademicDegreeCreateOrUpdateInput> GetAcademicDegreeForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new AcademicDegreeCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var academicDegree = await _academicDegreeRepository.GetAsync(input.Id.Value);
                if (academicDegree != null)
                    ObjectMapper.Map<AcademicDegree,AcademicDegreeCreateOrUpdateInput>(academicDegree,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_AcademicDegree_Create, AppPermissions.Pages_BaseInfo_AcademicDegree_Edit)]
        public async Task CreateOrUpdateAcademicDegree(AcademicDegreeCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateAcademicDegreeAsync(input);
            }
            else
            {
                await CreateAcademicDegreeAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_AcademicDegree_Delete)]
        public async Task DeleteAcademicDegree(EntityDto input)
        {
            try
            {
                await _academicDegreeRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseInfo_AcademicDegree_Edit)]
        private async Task UpdateAcademicDegreeAsync(AcademicDegreeCreateOrUpdateInput input)
        {
            var academicDegree = ObjectMapper.Map<AcademicDegree>(input);
            await _academicDegreeRepository.UpdateAsync(academicDegree);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseInfo_AcademicDegree_Create)]
        private async Task CreateAcademicDegreeAsync(AcademicDegreeCreateOrUpdateInput input)
        {
            var academicDegree = ObjectMapper.Map<AcademicDegree>(input);
            await _academicDegreeRepository.InsertAsync(academicDegree);
        }
        
        private IQueryable<AcademicDegree> GetFilteredQuery(GetAcademicDegreeInput input)
        {
            var query = QueryableExtensions.WhereIf(_academicDegreeRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(AcademicDegreeCreateOrUpdateInput input)
        {
            var existingObj = (await _academicDegreeRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _academicDegreeRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}