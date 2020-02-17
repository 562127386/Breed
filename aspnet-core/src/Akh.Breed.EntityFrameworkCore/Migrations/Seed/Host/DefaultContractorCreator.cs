using System.Linq;
using Microsoft.EntityFrameworkCore;
using Akh.Breed.Contractors;
using Akh.Breed.EntityFrameworkCore;

namespace Akh.Breed.Migrations.Seed.Host
{
    public class DefaultContractorCreator
    {
        private readonly BreedDbContext _context;

        public DefaultContractorCreator(BreedDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //CreateContractor();
        }

        private void CreateContractor()
        {
            var defaultContractor = _context.Contractors.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "پیمانکار 1");
            if (defaultContractor == null)
            {
                defaultContractor = new Contractor() { Name = "پیمانکار 1"};
                _context.Contractors.Add(defaultContractor);
                _context.SaveChanges();

            }
            
        }
    }
}