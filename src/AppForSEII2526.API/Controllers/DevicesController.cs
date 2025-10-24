using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    using AppForSEII2526.API.DTOs.RentDTOs; // DeviceForRentalDTO está en este espacio de nombres

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

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<DeviceForRentalDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDevices_ForRental_DTO(string? color, string? modelname)
        {
            var devices = await _context.Device
                .Where(d => (modelname == null || d.Model.NameModel.Contains(modelname)) && (color == null || d.Color.Contains(color)))
                .Select(d => new DeviceForRentalDTO(
                    d.Name,
                    d.Model,
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
