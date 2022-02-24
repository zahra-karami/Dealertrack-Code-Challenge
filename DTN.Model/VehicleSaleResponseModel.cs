namespace DTN.Models
{
    [Serializable]
    public class VehicleSaleResponseModel
    {
        public IList<VehicleSaleModel> List { set; get; }
        public string MostOftenSoldVehicle { get; set; }
    }
}
