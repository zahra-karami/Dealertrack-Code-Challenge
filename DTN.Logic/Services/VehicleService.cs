using DTN.Logic.Repositories.Interfaces;
using DTN.Logic.Services.Interfaces;
using DTN.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTN.Logic.Services
{
    public class VehicleService : BaseService, IVehicleService
    {
        private readonly IVehicleRepository _repository;
        public VehicleService(IVehicleRepository repository) : base()
        {
            _repository = repository;
        }
        public async Task AddNewFile(IList<VehicleModel> vehicles)
        {
            foreach (var vehicle in vehicles)
            {

                var exists = (await _repository.GetByDealNumber(vehicle.DealNumber)).Any();
                if (exists) continue;

                vehicle.Id = Guid.NewGuid();
                await _repository.Add(vehicle);
            }
        }

        public async Task DeleteDeal(Guid id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<VehicleModel>> GetAllVehicles()
        {
            return await _repository.GetAll();
        }

        public async Task<string> GetMostOftenSoldVehicle()
        {
            var list = await _repository.GetAll();

            var mostOftenSoldVehicle = list.GroupBy(c => c.Vehicle)
                    .OrderByDescending(gp => gp.Count())
                    .Take(1)
                    .Select(g => g.Key).FirstOrDefault();

            return mostOftenSoldVehicle;
        }
    }
}
