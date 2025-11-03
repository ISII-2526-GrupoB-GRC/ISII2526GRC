using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    using AppForSEII2526.API.DTOs.RentDTOs; // DeviceForRentalDTO está en este espacio de nombres
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(ApplicationDbContext context, ILogger<DevicesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get del Paso 2

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<DeviceForRentalDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDevicesForRental(string? nameModel, double? price)
        {
            var devices = await _context.Device
                .Where(d => (d.Model.NameModel == null || d.Model.NameModel.Contains(nameModel)) && (price == null || d.priceForRent == price))
                .Select(d => new DeviceForRentalDTO(
                    d.Name,
                    d.Model.NameModel,
                    d.Brand,
                    d.Year,
                    d.Color,
                    d.priceForRent
                ))
                .ToListAsync();
            return Ok(devices);
        }
    }



}
