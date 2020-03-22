using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.BaseInfo
{
    [Table("AkhSpeciesInfo")]
    public class SpeciesInfo : Entity, IHasCreationTime, IMayHaveTenant
    {
        public const long CodePrefix = 364052; 
        public const string CodePrefixStr = "364-0-52-"; 

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]      
        public string FromCodeStr
        {
            get
            {
                return Code.Length == 1
                    ? CodePrefixStr + Code + "-00000000"
                    : CodePrefixStr + Code[0] + "-" + Code[1] + "0000000";}
            private set { /* needed for EF */ }
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]      
        public string ToCodeStr
        {
            get
            {
                return Code.Length == 1
                    ? CodePrefixStr + Code + "-99999999"
                    : CodePrefixStr + Code[0] + "-" + Code[1] + "9999999";}
            private set { /* needed for EF */ }
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]      
        public long FromCode
        {
            get
            {
                return Code.Length == 1
                    ? (CodePrefix * 10 + Convert.ToInt64(Code)) * 100000000
                    : (CodePrefix * 100 + Convert.ToInt64(Code)) * 10000000;
            }
            private set { /* needed for EF */ }
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]      
        public long ToCode
        {
            get
            {
                return Code.Length == 1
                    ? (CodePrefix * 10 + Convert.ToInt64(Code)) * 100000000 + 99999999
                    : (CodePrefix * 100 + Convert.ToInt64(Code)) * 10000000 + 9999999;
            }
            private set { /* needed for EF */ }
        }
        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }

        public SpeciesInfo()
        {
            CreationTime = Clock.Now;
        }
    }
}