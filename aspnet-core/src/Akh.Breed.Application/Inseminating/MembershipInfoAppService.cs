using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.BaseInfo;
using Akh.Breed.Inseminating;
using Akh.Breed.Inseminating.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Inseminating
{
    public class MembershipInfoAppService :  BreedAppServiceBase, IMembershipInfoAppService
    {
        private readonly IRepository<MembershipInfo> _membershipInfoRepository;

        public MembershipInfoAppService(IRepository<MembershipInfo> membershipInfoRepository)
        {
            _membershipInfoRepository = membershipInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_MembershipInfo)]
        public async Task<PagedResultDto<MembershipInfoListDto>> GetMembershipInfo(GetMembershipInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var membershipInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var membershipInfosListDto = ObjectMapper.Map<List<MembershipInfoListDto>>(membershipInfos);
            return new PagedResultDto<MembershipInfoListDto>(
                userCount,
                membershipInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_MembershipInfo_Create, AppPermissions.Pages_Inseminating_MembershipInfo_Edit)]
        public async Task<MembershipInfoCreateOrUpdateInput> GetMembershipInfoForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new MembershipInfoCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var membershipInfo = await _membershipInfoRepository.GetAsync(input.Id.Value);
                if (membershipInfo != null)
                    ObjectMapper.Map<MembershipInfo,MembershipInfoCreateOrUpdateInput>(membershipInfo,output);
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_MembershipInfo_Create, AppPermissions.Pages_Inseminating_MembershipInfo_Edit)]
        public async Task CreateOrUpdateMembershipInfo(MembershipInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateMembershipInfoAsync(input);
            }
            else
            {
                await CreateMembershipInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_MembershipInfo_Delete)]
        public async Task DeleteMembershipInfo(EntityDto input)
        {
            try
            {
                await _membershipInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();            
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Inseminating_MembershipInfo_Edit)]
        private async Task UpdateMembershipInfoAsync(MembershipInfoCreateOrUpdateInput input)
        {
            var membershipInfo = ObjectMapper.Map<MembershipInfo>(input);
            await _membershipInfoRepository.UpdateAsync(membershipInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Inseminating_MembershipInfo_Create)]
        private async Task CreateMembershipInfoAsync(MembershipInfoCreateOrUpdateInput input)
        {
            var membershipInfo = ObjectMapper.Map<MembershipInfo>(input);
            await _membershipInfoRepository.InsertAsync(membershipInfo);
        }
        
        private IQueryable<MembershipInfo> GetFilteredQuery(GetMembershipInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_membershipInfoRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(MembershipInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _membershipInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _membershipInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name == input.Name));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
        }
    }
}