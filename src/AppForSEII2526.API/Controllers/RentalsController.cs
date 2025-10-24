using AppForSEII2526.API.DTOs.RentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<RentalsController> _logger;

        public RentalsController(ApplicationDbContext context, ILogger<RentalsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Método Get

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult> Get_Rental_Detail_DTO(int id)
        {
            if (_context.Rental == null)
            {
                _logger.LogError("Error: Rental table does not exist");
                return NotFound();
            }

            var rental = await _context.Rental
                .Where(r => r.Id == id)
                .Include(r => r.ApplicationUser)   // join tabla ApplicationUser
                .Include(r => r.RentDevice)        // join table RentDevice
                    .ThenInclude(rd => rd.Devices) // después join tabla Devices
                .Select(r => new RentalDetailDTO(
                    r.ApplicationUser.Name,
                    r.ApplicationUser.Surname,
                    r.ApplicationUser.DeliveryAddress,
                    r.RentalDate,
                    r.TotalPrice,
                    r.RentalDateFrom,
                    r.RentalDateTo,
                    r.RentDevice.Devices
                        .Select(rd => new RentDeviceDTO(
                            rd.Model.NameModel,          // model
                            rd.priceForRent,             // price for rent
                            r.RentDevice.Quantity        // quantity
                        )).ToList()
                ))
                .FirstOrDefaultAsync();


            if (rental == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }

            return Ok(rental);
        }

    }
}
