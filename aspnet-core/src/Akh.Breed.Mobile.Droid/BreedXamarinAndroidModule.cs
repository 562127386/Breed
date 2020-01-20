using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Akh.Breed
{
    [DependsOn(typeof(BreedXamarinSharedModule))]
    public class BreedXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BreedXamarinAndroidModule).GetAssembly());
        }
    }
}
