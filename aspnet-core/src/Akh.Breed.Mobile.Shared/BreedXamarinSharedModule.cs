using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Akh.Breed
{
    [DependsOn(typeof(BreedClientModule), typeof(AbpAutoMapperModule))]
    public class BreedXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BreedXamarinSharedModule).GetAssembly());
        }
    }
}
