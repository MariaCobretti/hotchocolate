using System;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace HotChocolate.ApolloFederation
{
    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Interface)]
    public class ForeignServiceTypeExtensionAttribute : ObjectTypeDescriptorAttribute
    {
        public override void OnConfigure(IDescriptorContext context, IObjectTypeDescriptor descriptor, Type type)
        {
            descriptor
                .Extend()
                .OnBeforeCreate(d => d.ContextData[WellKnownContextData.ExtendMarker] = true);
        }
    }
}
