using System.Collections.Generic;
using Akh.Breed.Caching.Dto;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}
