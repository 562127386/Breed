using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Akh.Breed
{
    [DependsOn(typeof(BreedXamarinSharedModule))]
    public class BreedXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BreedXamarinIosModule).GetAssembly());
        }
    }
}
