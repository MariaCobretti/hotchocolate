using HotChocolate.Types;

namespace HotChocolate.ApolloFederation
{
    public class ServiceType: ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor
                .Name(WellKnownTypeNames.Service)
                .Field("sdl")
                .Type<StringType>()
                .Resolve(resolver => FederationSchemaPrinter.Print(resolver.Schema));
        }
    }
}
