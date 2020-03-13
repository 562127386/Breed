using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueOfficerCreateOrUpdateInput
    {
        public const int CodeLength = 15; 
        
        public int? Id { get; set; }
        
        public int PlaqueCount { get; set; }

        public long FromCode { get; set; }
        
        public long ToCode { get; set; }
        
        public int? OfficerId { get; set; }
        
        public int? PlaqueStoreId { get; set; }

        public long? FinishedPlaqueId { get; set; }
    }
}