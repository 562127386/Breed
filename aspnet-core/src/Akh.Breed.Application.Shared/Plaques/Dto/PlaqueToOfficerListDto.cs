﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Plaques.Dto
{
    public class GetPlaqueToOfficerInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
            Sorting = Sorting.Replace("stateInfoName", "PlaqueToContractor.Contractor.CityInfo.StateInfo.Name");
            Sorting = Sorting.Replace("contractorName", "PlaqueToContractor.Contractor.Family,PlaqueToContractor.Contractor.Name");
            Sorting = Sorting.Replace("officerName", "Officer.Name");
            Sorting = Sorting.Replace("speciesName", "PlaqueToContractor.PlaqueToState.PlaqueStore.Species.Name");

            Filter = Filter?.Trim();
        }
    }
    
    public class PlaqueToOfficerListDto : EntityDto
    {
        public long FromCode { get; set; }
        
        public long ToCode { get; set; }
        public int PlaqueCount { get; set; }

        public int PlaqueUsed { get; set; }

        public DateTime SetTime { get; set; }
        
        public string StateName { get; set; }
        
        public string ContractorName { get; set; }
        
        public string OfficerName { get; set; }
        
        public string OfficerFamily { get; set; }
        
        public string OfficerCode { get; set; }

        public string SpeciesName { get; set; }

        public string CreationTimeStr { get; set; }
        
    }
}