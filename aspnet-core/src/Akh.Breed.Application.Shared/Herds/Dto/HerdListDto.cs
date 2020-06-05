using System;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Herds.Dto
{
    public class GetHerdInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
            Sorting = Sorting.Replace("liveStockOwner", "Family");
            Sorting = Sorting.Replace("officerName", "Officer.Family");
            Sorting = Sorting.Replace("contractorName", "Contractor.Family");
            Filter = Filter?.Trim();
        }
    }
    public class HerdListDto : EntityDto
    {
        public string HerdName { get; set; }
        
        public string AgriculturalId { get; set; }

        public string Code { get; set; }
        
        public string Institution { get; set; }

        public string LiveStockOwner { get; set; }

        public string OfficerName { get; set; }

        public string EpidemiologicCode { get; set; }

        public string PostalCode { get; set; }

        public string Mobile { get; set; }

        public string ActivityInfoName { get; set; }

        public string ContractorName { get; set; }
        
        public bool IsCertificated { get; set; }
    }
}