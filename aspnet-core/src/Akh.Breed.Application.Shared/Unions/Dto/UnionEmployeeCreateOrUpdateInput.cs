using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Unions.Dto
{
    public class UnionEmployeeCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string NationalCode { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }
        
        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Post { get; set; }

        [Required]
        public int UnionInfoId { get; set; }

        public long UserId { get; set; }
    }
}