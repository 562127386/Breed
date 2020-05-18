﻿using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class GetVillageInfoInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class VillageInfoListDto : EntityDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int RegionInfoId { get; set; }
        
        public string StateInfoName { get; set; }
        
        public string CityInfoName { get; set; }
        
        public string RegionInfoName { get; set; }
    }
}