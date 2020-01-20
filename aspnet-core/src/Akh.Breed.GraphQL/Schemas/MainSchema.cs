using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using Akh.Breed.Queries.Container;

namespace Akh.Breed.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}
