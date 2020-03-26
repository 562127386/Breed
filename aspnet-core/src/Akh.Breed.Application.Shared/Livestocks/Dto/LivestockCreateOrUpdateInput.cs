using System;
using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Livestocks.Dto
{
    public class LivestockCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string NationalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public bool Imported { get; set; }

        public DateTime? BirthDate { get; set; }
        
        public int? SpeciesInfoId { get; set; }

        public int? SexInfoId { get; set; }

        public int? HerdId { get; set; }

        public int? ActivityInfoId { get; set; }

        public int? OfficerId { get; set; }

    }
}