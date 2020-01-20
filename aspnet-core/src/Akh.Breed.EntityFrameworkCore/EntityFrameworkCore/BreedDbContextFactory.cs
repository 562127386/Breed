using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Akh.Breed.Configuration;
using Akh.Breed.Web;

namespace Akh.Breed.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class BreedDbContextFactory : IDesignTimeDbContextFactory<BreedDbContext>
    {
        public BreedDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BreedDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            BreedDbContextConfigurer.Configure(builder, configuration.GetConnectionString(BreedConsts.ConnectionStringName));

            return new BreedDbContext(builder.Options);
        }
    }
}
