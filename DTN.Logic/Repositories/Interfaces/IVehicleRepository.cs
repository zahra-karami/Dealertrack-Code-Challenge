using DTN.Models;

namespace DTN.Logic.Repositories.Interfaces
{
    public interface IVehicleRepository
    {

        Task Add(VehicleModel vehicle);      
        Task<List<VehicleModel>> GetAll();
    }
}
