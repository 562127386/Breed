using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Akh.Breed.Authorization;

namespace Akh.Breed
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(BreedApplicationSharedModule),
        typeof(BreedCoreModule)
        )]
    public class BreedApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BreedApplicationModule).GetAssembly());
        }
    }
}
