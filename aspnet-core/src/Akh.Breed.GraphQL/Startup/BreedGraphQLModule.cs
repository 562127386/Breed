using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Akh.Breed.Startup
{
    [DependsOn(typeof(BreedCoreModule))]
    public class BreedGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BreedGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}
