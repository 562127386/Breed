using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class StateImportDto
    {
        public string StateName { get; set; }

        public string StateCode { get; set; }
        
        public string CityName { get; set; }

        public string CityCode { get; set; }
        
        public string VillageName { get; set; }

        public string VillageCode { get; set; }
        
        public string Exception { get; set; }

        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}