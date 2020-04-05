using System;
using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Herds.Dto
{
    public class HerdGeoLogCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }
        
        public int? HerdId { get; set; }
        
        public int? OfficerId { get; set; }
    }
}