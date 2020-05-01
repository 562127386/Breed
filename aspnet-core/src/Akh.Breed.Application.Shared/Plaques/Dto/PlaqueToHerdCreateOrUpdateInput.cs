using System;
using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueToHerdCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string NationalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }
        
        public string OfficerName { get; set; }
        public int? OfficerId { get; set; }
        
        public int? HerdId { get; set; }
        
        public DateTime? CreationTime { get; set; }

    }
}