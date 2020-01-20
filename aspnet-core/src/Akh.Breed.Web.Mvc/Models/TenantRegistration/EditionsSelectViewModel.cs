using Abp.AutoMapper;
using Akh.Breed.MultiTenancy.Dto;

namespace Akh.Breed.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}

