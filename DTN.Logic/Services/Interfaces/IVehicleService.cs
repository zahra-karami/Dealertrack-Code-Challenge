using DTN.Models;


namespace DTN.Logic.Services.Interfaces
{
    public interface IVehicleService
    {        
        Task AddNewFile(IList<VehicleModel> vehicles);
        Task<IEnumerable<VehicleModel>> GetAllVehicles();
        Task<string> GetMostOftenSoldVehicle();
    }
}
