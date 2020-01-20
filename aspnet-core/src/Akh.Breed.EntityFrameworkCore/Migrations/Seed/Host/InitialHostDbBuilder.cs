using Akh.Breed.EntityFrameworkCore;

namespace Akh.Breed.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly BreedDbContext _context;

        public InitialHostDbBuilder(BreedDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}

