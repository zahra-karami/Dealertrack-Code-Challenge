using DTN.Models;
using Microsoft.AspNetCore.Mvc;
using DTN.Logic.Services.Interfaces;

namespace DTN.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;

        private readonly IVehicleService _vehicleService;
        private readonly IFileService _fileService;

        public VehicleController(ILogger<VehicleController> logger, IFileService fileService, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
            _fileService = fileService;


        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                var response = new ResponseModel<VehicleGetResponseModel>();
                var file = Request.Form.Files.FirstOrDefault();

                response.ResponseMessage = _fileService.Validate(file);
                if (response.ResponseMessage.Count > 0) return BadRequest(response);

                var list = await _fileService.Deserialize(file);
                await _vehicleService.AddNewFile(list);
                var mostOftenSoldVehicle = await _vehicleService.GetMostOftenSoldVehicle();

                response.Result = new VehicleGetResponseModel { List = list, MostOftenSoldVehicle= mostOftenSoldVehicle };
                response.IsSucceeded = true;
                response.ResponseCode = 200;

                _logger.LogInformation($"File {file.FileName} Uploaded Successfully");
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Vehicle.FileUpload");
                return StatusCode(500, $"Internal server error");

            }
        }



        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetVehicle()
        {
            try
            {
                var response = new ResponseModel<VehicleGetResponseModel>();

                var list = await _vehicleService.GetAllVehicles();
                var mostOftenSoldVehicle = await _vehicleService.GetMostOftenSoldVehicle();



                response.Result = new VehicleGetResponseModel
                {
                    List = list.ToList(),
                    MostOftenSoldVehicle = mostOftenSoldVehicle
                };

                response.IsSucceeded = true;
                response.ResponseCode = 200;
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on VehicleSale.FileUpload");
                return StatusCode(500, $"Internal server error: {ex}");

            }
        }


        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteDeal(Guid id)
        {
            try
            {
                var response = new ResponseModel<string>();
                await _vehicleService.DeleteDeal(id);
                response.Result = "success";
                response.IsSucceeded = true;
                response.ResponseCode = 200;
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on VehicleSale.FileUpload");
                return StatusCode(500, $"Internal server error: {ex}");

            }
        }
    }
}
