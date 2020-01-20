using System.Collections.Generic;
using Akh.Breed.DashboardCustomization.Dto;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.CustomizableDashboard
{
    public class AddWidgetViewModel
    {
        public List<WidgetOutput> Widgets { get; set; }

        public string DashboardName { get; set; }

        public string PageId { get; set; }
    }
}

