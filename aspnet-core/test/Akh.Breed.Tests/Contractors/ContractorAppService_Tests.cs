using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Akh.Breed.Contractors;
using Microsoft.EntityFrameworkCore;
using Akh.Breed.Editions;
using Akh.Breed.Editions.Dto;
using Akh.Breed.Features;
using Akh.Breed.Test.Base;
using Shouldly;
using Xunit;

namespace Akh.Breed.Tests.Contractors
{
    public class ContractorAppService_Tests : AppTestBase
    {
        private readonly IRepository<Contractor,long> _contractorRepository;

        public ContractorAppService_Tests()
        {
            LoginAsHostAdmin();

            _contractorRepository = Resolve<IRepository<Contractor,long>>();
        }
        
        [MultiTenantFact]
        public async Task Should_Get_Contractors()
        {
            var contractors = await _contractorRepository.GetAllListAsync();
            contractors.Count.ShouldBeGreaterThan(0);
        }
    }
}