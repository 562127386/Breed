using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;

namespace Akh.Breed.BaseInfos.Dto
{
    public class StateInfoCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }
    }
}