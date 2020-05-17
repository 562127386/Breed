using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Unions.Dto
{
    public class UnionInfoCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        [Required]
        public string UnionName { get; set; }

        [Required]
        public string Code { get; set; }

        public string NationalCode { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }
        
        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        [Required]
        public int StateInfoId { get; set; }

        public long UserId { get; set; }
    }
}