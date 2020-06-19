using System;

namespace Akh.Breed.Inseminating.Dto
{
    public class InseminationCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string NationalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public DateTime? BirthDate { get; set; }
        
        public int? SpeciesInfoId { get; set; }

        public int? SexInfoId { get; set; }

        public int? HerdId { get; set; }

        public int? ActivityInfoId { get; set; }

        public int? OfficerId { get; set; }
        
        public string OfficerName { get; set; }
        
        public int? BreedInfoId { get; set; }

        public int? LivestockFatherId { get; set; }
        public string NationalCodeFather { get; set; }
        public int? BreedInfoFatherId { get; set; }

        public int? LivestockMotherId { get; set; }
        public string NationalCodeMother { get; set; }
        public int? BreedInfoMotherId { get; set; }
        
        public string EarNumber { get; set; }
        
        public string BodyNumber { get; set; }
        
        public string ForeignRegistrationNumber { get; set; }
        
        public int? BirthTypeInfoId { get; set; }
        
        public int? AnomalyInfoId { get; set; }
        
        public int? MembershipInfoId { get; set; }

        public DateTime? IdIssueDate { get; set; }
        
        public string BloodShare { get; set; }
        
        public string BreedShare { get; set; }
        
        public int? BodyColorInfoId { get; set; }
        
        public int? SpotColorInfoId { get; set; }
        
        public int? SpotConnectorInfoId { get; set; }

        public string BreedName { get; set; }

        public DateTime? CreationTime { get; set; }

    }
}