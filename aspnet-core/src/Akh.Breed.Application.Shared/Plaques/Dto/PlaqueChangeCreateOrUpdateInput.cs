using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Timing;

namespace Akh.Breed.Plaques.Dto
{
    public class PlaqueChangeCreateOrUpdateInput
    {
        public const int CodeLength = 15; 
        
        public int? Id { get; set; }
       
        public string PlaqueCode { get; set; }
        
        public string PlaqueHerdName { get; set; }
        
        public long? PlaqueId { get; set; }

        public string ChangeReason { get; set; }

        public string PreStateName { get; set; }
        
        public int? PreStateId { get; set; }
        
        public int? NewStateId { get; set; }
        
        public int? OfficerId { get; set; }
        
    }
}