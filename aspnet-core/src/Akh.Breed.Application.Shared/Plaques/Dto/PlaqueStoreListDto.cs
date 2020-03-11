﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class GetPlaqueStoreInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "FromCode";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class PlaqueStoreListDto : EntityDto
    {
        public string FromCode { get; set; }
        
        public string ToCode { get; set; }
        
        public DateTime SetTime { get; set; }

        public string SpeciesName { get; set; }

        public bool Finished { get; set; }
        
        public string FinishedCode { get; set; }
        
        public DateTime? FinishedDate { get; set; }
    }
}