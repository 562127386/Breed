using Abp.AutoMapper;
using Akh.Breed.MultiTenancy;
using Akh.Breed.MultiTenancy.Dto;
using Akh.Breed.Web.Areas.AppAreaName.Models.Common;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}
