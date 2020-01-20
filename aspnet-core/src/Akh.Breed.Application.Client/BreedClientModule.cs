using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Akh.Breed
{
    public class BreedClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BreedClientModule).GetAssembly());
        }
    }
}

