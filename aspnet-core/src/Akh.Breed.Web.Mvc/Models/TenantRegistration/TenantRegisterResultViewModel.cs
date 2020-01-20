using Abp.AutoMapper;
using Akh.Breed.MultiTenancy.Dto;

namespace Akh.Breed.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}
