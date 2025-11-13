using AppForSEII2526.API.DTOs.RentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{

    using AppForSEII2526.API.DTOs.RentDTOs;
    using AppForSEII2526.API.Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Drawing.Drawing2D;
    using static AppForSEII2526.API.Models.PaymentMethodTypes;

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

        public async Task<ActionResult> GetRentalDetail(int id)
        {
            if (_context.Rental == null)
            {
                _logger.LogError("Error: Rental table does not exist");
                return NotFound();
            }

            var rental = await _context.Rental
                .Where(r => r.Id == id)
                .Include(a => a.ApplicationUser)
                .Include(rd => rd.RentDevices)
                    .ThenInclude(d => d.Device)
                        .ThenInclude(m => m.Model)
                .Select(r => new RentalDetailDTO(
                    r.ApplicationUser.Name,
                    r.ApplicationUser.Surname,
                    r.DeliveryAddress,
                    r.RentalDate,
                    r.TotalPrice,
                    r.RentalDateFrom,
                    r.RentalDateTo,
                    r.RentDevices.Select(rd => new RentalItemDTO
                    (
                        rd.Device.Brand,            // Marca
                        rd.Device.Model.NameModel,  // Modelo
                        rd.Price,     // Precio
                        rd.Quantity   // Cantidad
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