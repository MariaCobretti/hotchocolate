using HotChocolate.Types;
using Snapshooter.Xunit;
using Xunit;

namespace HotChocolate.ApolloFederation
{
    public class FederationSchemaPrinterTests
    {
        [Fact(Skip = "WIP")]
        public void TestFederationPrinterApolloDirectivesSchemaFirst()
        {
            // arrange
            ISchema schema = SchemaBuilder.New()
                .AddApolloFederation()
                .AddDocumentFromString(
                @"
                extend type TestType @key(fields: ""id"") {
                    id: Int!
                    name: String!
                }

                type Query {
                    someField(a: Int): TestType
                }")
                .Use(next => context => default)
                .Create();

            // assert
            FederationSchemaPrinter.Print(schema).MatchSnapshot();
        }

        [Fact]
        public void TestFederationPrinterApolloDirectivesPureCodeFirst()
        {
            // arrange
            ISchema schema = SchemaBuilder.New()
                .AddApolloFederation()
                .AddQueryType<QueryRoot<User>>()
                .Create();

            // check
            FederationSchemaPrinter.Print(schema).MatchSnapshot();
        }

        [Fact]
        public void TestFederationPrinterTypeExtensionPureCodeFirst()
        {
            // arrange
            ISchema schema = SchemaBuilder.New()
                .AddApolloFederation()
                .AddQueryType<QueryRoot<Product>>()
                .Create();

            // check
            FederationSchemaPrinter.Print(schema).MatchSnapshot();
        }

        public class QueryRoot<T>
        {
            public T GetEntity(int id) => default;
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

        [ForeignServiceTypeExtension]
        public class Product
        {
            [Key]
            public string Upc { get; set; }
        }
    }
}
