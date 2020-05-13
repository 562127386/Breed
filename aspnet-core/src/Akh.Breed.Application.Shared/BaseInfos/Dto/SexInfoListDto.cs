﻿using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.BaseInfos.Dto
{
    public class GetSexInfoInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime Desc";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class SexInfoListDto : EntityDto
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}