using Microsoft.AspNetCore.Mvc;

namespace DTN.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VehicleSaleController : ControllerBase
    {
        private readonly ICsvSerializer<VehicleSaleModel> _serializer;
        private readonly IFileValidator _fileValidator;

        public IActionResult Index()
        {
            return View();
        }
    }
}
