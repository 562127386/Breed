using System.Linq;
using Akh.Breed.Contractors;
using Akh.Breed.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Migrations.Seed.Host
{
    public class DefaultBaseInfoCreator
    {
        private readonly BreedDbContext _context;

        public DefaultBaseInfoCreator(BreedDbContext context)
        {
            _context = context;
        }
        
        public void Create()
        {
            //CreateAcademicDegree();
        }
        
        private void CreateAcademicDegree()
        {
            var defaultContractor = _context.Contractors.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "پیمانکار 1");
            if (defaultContractor == null)
            {
                defaultContractor = new Contractor(ContractorFirmType.AgricultureFirm) { Name = "پیمانکار 1"};
                _context.Contractors.Add(defaultContractor);
                _context.SaveChanges();

            }
            
        }
    }
}