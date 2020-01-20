using System.Collections.Generic;
using Abp.Localization;
using Akh.Breed.Install.Dto;

namespace Akh.Breed.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}

