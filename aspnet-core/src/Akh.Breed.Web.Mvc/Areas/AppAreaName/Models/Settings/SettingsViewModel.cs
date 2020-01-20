using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Akh.Breed.Configuration.Tenants.Dto;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.Settings
{
    public class SettingsViewModel
    {
        public TenantSettingsEditDto Settings { get; set; }
        
        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}
