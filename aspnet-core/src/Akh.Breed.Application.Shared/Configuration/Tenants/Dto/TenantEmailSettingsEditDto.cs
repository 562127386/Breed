using Abp.Auditing;
using Akh.Breed.Configuration.Dto;

namespace Akh.Breed.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}
