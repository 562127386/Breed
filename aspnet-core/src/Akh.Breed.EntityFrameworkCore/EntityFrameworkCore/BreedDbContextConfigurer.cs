using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.EntityFrameworkCore
{
    public static class BreedDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BreedDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BreedDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
