using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Akh.Breed.Configure;
using Akh.Breed.Startup;
using Akh.Breed.Test.Base;

namespace Akh.Breed.GraphQL.Tests
{
    [DependsOn(
        typeof(BreedGraphQLModule),
        typeof(BreedTestBaseModule))]
    public class BreedGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BreedGraphQLTestModule).GetAssembly());
        }
    }
}
