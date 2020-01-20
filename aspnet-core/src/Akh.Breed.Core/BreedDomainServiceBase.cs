using Abp.Domain.Services;

namespace Akh.Breed
{
    public abstract class BreedDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected BreedDomainServiceBase()
        {
            LocalizationSourceName = BreedConsts.LocalizationSourceName;
        }
    }
}

