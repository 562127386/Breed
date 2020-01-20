using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Akh.Breed.Configuration.Host.Dto;
using Akh.Breed.Editions.Dto;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}
