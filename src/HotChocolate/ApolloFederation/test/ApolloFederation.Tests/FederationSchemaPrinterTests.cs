using HotChocolate.Types;
using Snapshooter.Xunit;
using Xunit;

namespace HotChocolate.ApolloFederation
{
    public class FederationSchemaPrinterTests
    {
        [Fact]
        public void TestFederationPrinterApolloDirectivesSchemaFirst()
        {
            // arrange
            // todo

            // act
        }

        [Fact]
        public void TestFederationPrinterApolloDirectivesPureCodeFirst()
        {
            // arrange
            ISchema schema = SchemaBuilder.New()
                .AddApolloFederation()
                .AddQueryType<Query>()
                .Create();

            // check
            FederationSchemaPrinter.Print(schema).MatchSnapshot();
        }

        public class Query
        {
            public User GetEntity(int id) => default;
        }

        public class User
        {
            [Key]
            public int Id { get; set; }
            [External]
            public string IdCode { get; set; }
            [Requires("idCode")]
            public string IdCodeShort { get; set; }
            [Provides("zipcode")]
            public Address Address { get; set; }
        }

        public class Address
        {
            [External]
            public string Zipcode { get; set; }
        }
    }
}
