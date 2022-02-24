

using DTN.Logic.Helpers.Interfaces;
using DTN.Logic.Services.Interfaces;
using DTN.Models;
using Microsoft.AspNetCore.Http;

namespace DTN.Logic.Services
{
    public class FileService : BaseService, IFileService
    {
        private readonly ICsvSerializer<VehicleModel> _serializer;
        private readonly IFileValidator _fileValidator;
        public FileService(ICsvSerializer<VehicleModel> serializer, IFileValidator fileValidator):base()
        {
            _fileValidator = fileValidator;

            _serializer = serializer;
            _serializer.UseLineNumbers = false;
            _serializer.UseTextQualifier = true;
        }
        public async Task<IList<VehicleModel>> Deserialize(IFormFile file)
        {
            return await _serializer.DeserializeAsync(file.OpenReadStream());
        }

        public List<string> Validate(IFormFile file)
        {
           return _fileValidator.Validate(file);
        }
    }
}
