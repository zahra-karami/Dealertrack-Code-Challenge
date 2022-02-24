using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DTN.Logic.Repositories.Interfaces;
using DTN.Models;
using Microsoft.Extensions.Logging;

namespace DTN.Logic.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {

        private readonly IDynamoDBContext _context;
        private readonly ILogger<VehicleRepository> _logger;

        public VehicleRepository(ILogger<VehicleRepository> logger, IDynamoDBContext dynamoDbContext)
        {
            _context = dynamoDbContext;
            _logger = logger;
        }

        public async Task Add(VehicleModel vehicle)
        {
            try
            {
                await _context.SaveAsync(vehicle);
            }
            catch (Exception ex)
            {
                if (ex is AmazonDynamoDBException)
                {
                    Console.WriteLine(ex.Message);
                }

                _logger.LogError(ex, "Error on VehicleRepository");
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            await _context.DeleteAsync<VehicleModel>(id);
        }

        public async Task<List<VehicleModel>> GetByDealNumber(int dealnumber)
        {

            var conditions = new List<ScanCondition>{
                new ScanCondition("DealNumber", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, dealnumber)
            };

            return await _context.ScanAsync<VehicleModel>(conditions).GetNextSetAsync();
        }

        public async Task<List<VehicleModel>> GetAll()
        {
            var conditions = new List<ScanCondition>();
            return await _context.ScanAsync<VehicleModel>(conditions).GetRemainingAsync();
        }
    }
}
