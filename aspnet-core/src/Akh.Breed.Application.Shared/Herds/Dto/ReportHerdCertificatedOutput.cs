using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Akh.Breed.Livestocks.Dto;

namespace Akh.Breed.Herds.Dto
{
    public class ReportHerdCertificatedOutput
    {
        public int Id { get; set; }
        
        public string HerdName { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }
        
        public string EpidemiologicCode { get; set; }

        public string ContractorName { get; set; }  
        
        public string OfficerName { get; set; }
        
        public List<LivestockListDto> Livestocks  { get; set; }
    }
}