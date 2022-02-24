using DTN.Models;

namespace DTN.Logic.Repositories.Interfaces
{
    public interface IVehicleRepository
    {

        Task Add(VehicleModel vehicle);      
        Task<List<VehicleModel>> GetAll();
        Task<List<VehicleModel>> GetByDealNumber(int dealnumber);
        Task Delete(Guid id);
    }
}
