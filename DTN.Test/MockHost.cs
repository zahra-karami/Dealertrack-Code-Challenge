using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTN.Test
{
    class MockHost : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                //services.RemoveAll(typeof(DbContextOptions<DynamoDBContext>));
                //services.AddDbContext<NotesDbContext>(options =>
                //    options.UseInMemoryDatabase("Testing", root));
            });

            return base.CreateHost(builder);
        }
    }

    public class MyTestFixture : Wadsworth.DynamoDB.TestUtilities.DynamoDBLocalFixture
    {
        protected override List<CreateTableRequest> GetCreateTableRequests()
        {
            return new List<CreateTableRequest>
            {
                new CreateTableRequest
                {
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition("Id", ScalarAttributeType.S),
                        new AttributeDefinition("dealNumber", ScalarAttributeType.S),
                        new AttributeDefinition("customerName", ScalarAttributeType.S),
                        new AttributeDefinition("dealershipName", ScalarAttributeType.S),
                        new AttributeDefinition("vehicle", ScalarAttributeType.S),
                        new AttributeDefinition("price", ScalarAttributeType.S),
                        new AttributeDefinition("date", ScalarAttributeType.S),
                    },
                    BillingMode = BillingMode.PAY_PER_REQUEST,
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement("Id", KeyType.HASH),
                    },
                    GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>
                    {
                        new GlobalSecondaryIndex
                        {
                            IndexName = "gsi1",
                            KeySchema = new List<KeySchemaElement>
                            {
                                new KeySchemaElement("gsi1_pk", KeyType.HASH),
                            },
                            Projection = new Projection
                            {
                                ProjectionType = ProjectionType.ALL
                            }
                        },
                    },
                    TableName = "DealerTrackVehicles"
                }
            };
        }

    }

}