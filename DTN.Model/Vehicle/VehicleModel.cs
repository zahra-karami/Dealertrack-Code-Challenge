using Amazon.DynamoDBv2.DataModel;

namespace DTN.Models
{
    [Serializable]
    [DynamoDBTable("DealerTrackVehicles")]
    public class VehicleModel
    {
        [DynamoDBProperty("Id")]
        [DynamoDBHashKey]
        public Guid Id { get; set; }
        [DynamoDBProperty("dealNumber")]
        public int DealNumber { set; get; }
        [DynamoDBProperty("customerName")]
        public string CustomerName { set; get; }
        [DynamoDBProperty("dealershipName")]
        public string DealershipName { set; get; }
        [DynamoDBProperty("vehicle")]
        public string Vehicle { set; get; }
        [DynamoDBProperty("price")]
        public decimal Price { set; get; }
        [DynamoDBProperty("date")]
        public DateTime Date { set; get; }

    }
}
