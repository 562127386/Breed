using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Akh.Breed.EntityFrameworkCore;

namespace Akh.Breed.HealthChecks
{
    public class BreedDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public BreedDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("BreedDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("BreedDbContext could not connect to database"));
        }
    }
}

