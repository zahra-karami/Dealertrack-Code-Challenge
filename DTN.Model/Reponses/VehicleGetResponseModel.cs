namespace DTN.Models
{
    [Serializable]
    public class VehicleGetResponseModel
    {
        public IList<VehicleModel> List { set; get; }
        public string MostOftenSoldVehicle { get; set; }
    }
}
