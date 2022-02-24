using DTN.Models;
using Microsoft.AspNetCore.Http;


namespace DTN.Logic.Services.Interfaces
{
    public interface IFileService
    {
        List<string> Validate(IFormFile file);

        Task<IList<VehicleModel>> Deserialize(IFormFile file);
    }
}
