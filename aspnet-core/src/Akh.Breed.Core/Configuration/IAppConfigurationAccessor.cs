using Microsoft.Extensions.Configuration;

namespace Akh.Breed.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}

