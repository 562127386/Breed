using System;
using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Support.Dto
{
    public class SupportCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string Description { get; set; }
        
        public string Response { get; set; }
        
        public int? SupportTypeId { get; set; }
        
        public int? SupportStateId { get; set; }
        
        public long? UserId { get; set; }
        
        public DateTime CreationTime { get; set; }
    }
}