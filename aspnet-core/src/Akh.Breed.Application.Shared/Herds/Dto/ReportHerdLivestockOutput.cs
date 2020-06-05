using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Akh.Breed.Livestocks.Dto;

namespace Akh.Breed.Herds.Dto
{
    public class ReportHerdLivestockOutput
    {
        public int Id { get; set; }
        
        public string DoneDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string HerdName { get; set; }
        
        public string Code { get; set; }
        
        public string NationalCode { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }
        
        public string FirmCode { get; set; }
        
        public string FirmName { get; set; }
        
        public string StateName { get; set; }
        
        public string CityName { get; set; }
        
        public string RegionName { get; set; }
        
        public string VillageName { get; set; }

        public string ContractorName { get; set; }  
        
        public string OfficerName { get; set; }
        
        public string OfficerCode { get; set; }
        
        public List<LivestockListDto> Livestocks  { get; set; }
    }
}