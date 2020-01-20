using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Akh.Breed
{
    [DependsOn(typeof(BreedCoreSharedModule))]
    public class BreedApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BreedApplicationSharedModule).GetAssembly());
        }
    }
}
