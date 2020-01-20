using Akh.Breed.Dto;

namespace Akh.Breed.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}
