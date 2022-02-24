using DTN.Models;
using DTN.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DTN.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VehicleSaleController : ControllerBase
    {
        private readonly ILogger<VehicleSaleController> _logger;
        private readonly ICsvSerializer<VehicleSaleModel> _serializer;
        private readonly IFileValidator _fileValidator;

        public VehicleSaleController(ILogger<VehicleSaleController> logger, ICsvSerializer<VehicleSaleModel> serializer, IFileValidator fileValidator)
        {
            _logger = logger;

            _fileValidator = fileValidator;

            _serializer = serializer;
            _serializer.UseLineNumbers = false;
            _serializer.UseTextQualifier = true;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                var response = new ResponseModel<VehicleSaleResponseModel>();
                var file = Request.Form.Files.FirstOrDefault();

                response.ResponseMessage = _fileValidator.Validate(file);
                if (response.ResponseMessage.Count > 0) return BadRequest(response);


                var list = await _serializer.DeserializeAsync(file.OpenReadStream());
                var mostOftenSoldVehicle = list.GroupBy(c => c.Vehicle)
                    .OrderByDescending(gp => gp.Count())
                    .Take(1)
                    .Select(g => g.Key).FirstOrDefault();


                response.IsSucceeded = true;
                response.Result = new VehicleSaleResponseModel
                {
                    List = list,
                    MostOftenSoldVehicle = mostOftenSoldVehicle
                };
                response.ResponseCode = 200;

                _logger.LogInformation($"File {file.FileName} Uploaded Successfully");
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error on VehicleSale.FileUpload");
                return StatusCode(500, $"Internal server error: {ex}");

            }
        }
    }
}
