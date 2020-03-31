using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Akh.Breed.Notices;
using Akh.Breed.Notices.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Notices
{
    public class NoticeAppService :  BreedAppServiceBase, INoticeAppService
    {
        private readonly IRepository<Notice> _noticeRepository;

        public NoticeAppService(IRepository<Notice> noticeRepository)
        {
            _noticeRepository = noticeRepository;
        }

        public async Task<PagedResultDto<NoticeListDto>> GetNotice(GetNoticeInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var notices = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var noticesListDto = ObjectMapper.Map<List<NoticeListDto>>(notices);
            return new PagedResultDto<NoticeListDto>(
                userCount,
                noticesListDto
            );
        }
        
        public async Task<NoticeCreateOrUpdateInput> GetNoticeForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new NoticeCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var notice = await _noticeRepository.GetAsync(input.Id.Value);
                if (notice != null)
                    ObjectMapper.Map<Notice,NoticeCreateOrUpdateInput>(notice,output);
            }

            return output;
        }
        
        public async Task CreateOrUpdateNotice(NoticeCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateNoticeAsync(input);
            }
            else
            {
                await CreateNoticeAsync(input);
            }
        }
        
        public async Task DeleteNotice(EntityDto input)
        {
            await _noticeRepository.DeleteAsync(input.Id);
        }
        
        public async Task ToggleNotice(int? input)
        {
            var notice = await _noticeRepository.GetAsync(input.Value);
            notice.IsEnabled = !notice.IsEnabled;
            notice.UserId = AbpSession.UserId;
            await _noticeRepository.UpdateAsync(notice);
        }

        private async Task UpdateNoticeAsync(NoticeCreateOrUpdateInput input)
        {
            var notice = ObjectMapper.Map<Notice>(input);
            notice.UserId = AbpSession.UserId;
            await _noticeRepository.UpdateAsync(notice);
        }
        
        private async Task CreateNoticeAsync(NoticeCreateOrUpdateInput input)
        {
            var notice = ObjectMapper.Map<Notice>(input);
            notice.UserId = AbpSession.UserId;
            await _noticeRepository.InsertAsync(notice);
        }
        
        private IQueryable<Notice> GetFilteredQuery(GetNoticeInput input)
        {
            var query = QueryableExtensions.WhereIf(_noticeRepository.GetAll()
                    .Include(x => x.User),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Title.Contains(input.Filter) ||
                    u.Message.Contains(input.Filter));

            return query;
        }
        
        private async Task CheckValidation(NoticeCreateOrUpdateInput input)
        {
            var existingObj = (await _noticeRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Title == input.Title));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
        }
    }
}