using Abp.AutoMapper;
using Akh.Breed.Organizations.Dto;

namespace Akh.Breed.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}
