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

        // Método Create / Post

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateRental(RentalForCreateDTO rentalForCreate)
        {
            if (rentalForCreate.Name == null) ModelState.AddModelError("Name", "El campo Nombre es obligatorio");

            var user = _context.ApplicationUser.FirstOrDefault(au => au.UserName == rentalForCreate.Name);
            if (user == null) ModelState.AddModelError("RentalApplicationUser", "Error! UserName no registrado");

            if (rentalForCreate.Surname == null) ModelState.AddModelError("Surname", "El campo Apellido es obligatorio");

            //Modificación del examen:
            if (rentalForCreate.DeliveryAddress == null) ModelState.AddModelError("DeliveryAddress", "El campo Dirección es obligatorio");
            else if
                (!rentalForCreate.DeliveryAddress.Contains("Calle") || !rentalForCreate.DeliveryAddress.Contains("Carretera")){
                ModelState.AddModelError("DeliveryAddress", "Error en la direción de envío. Por favor introduce una dirección válida incluyendo las palabras clave Calle o Carretera");
            }
            //
            if (rentalForCreate.RentalDateFrom <= DateTime.Today)
                ModelState.AddModelError("RentalDateFrom", "Error! La fecha de alquiler debe comenzar más tarde de hoy");

            if (rentalForCreate.RentalDateFrom >= rentalForCreate.RentalDateTo)
                ModelState.AddModelError("RentalDateFrom&RentalDateTo", "Error! El alquiler debe acabar más tarde de la fecha de comienzo");

            if (rentalForCreate.RentalItems.Count == 0)
                ModelState.AddModelError("RentalItems", "Error! Se debe introducir al menos un dispositivo para alquilar");

            //  condiciones que pueden llevar a error en la creación del alquiler

            if (ModelState.ErrorCount > 0) return BadRequest(new ValidationProblemDetails(ModelState)); //Si hay errores, la entrada es incorrecta

            var deviceModel = rentalForCreate.RentalItems.Select(ri => ri.NameModel).ToList<string>(); //Se obtienen los modelos de los dispositivos a alquilar

            var devices = await _context.Device
                .Include(d => d.Model)
                .Include(d => d.RentedDevices)
                    .ThenInclude(rd => rd.Rental)
                .Where(d => deviceModel.Contains(d.Model.NameModel))

                .Select(d => new
                {
                    d.Id,
                    d.Model.NameModel,
                    d.Brand,
                    d.priceForRent,
                    d.quantityForRent,
                    NumberOfRentedItems = d.RentedDevices
                        .Where(rd =>
                            (rd.Rental.RentalDateFrom < rentalForCreate.RentalDateTo) &&
                            (rd.Rental.RentalDateTo > rentalForCreate.RentalDateFrom))
                        .Sum(rd => rd.Quantity)
                })
                .ToListAsync();

            //Creación del alquiler
            Rental rental = new Rental(rentalForCreate.DeliveryAddress, DateTime.Today, rentalForCreate.PaymentMethod, rentalForCreate.RentalDateFrom, rentalForCreate.RentalDateTo, new List<RentDevice>(), user);

            var numDays = (rental.RentalDateTo - rental.RentalDateFrom).TotalDays;

            foreach (var item in rentalForCreate.RentalItems)
            {
                //Buscar por Modelo Y Marca
                var device = devices.FirstOrDefault(d => d.NameModel == item.NameModel && d.Brand == item.Brand);

                //Check de que hay cantidad adecuada
                if ((device == null) || (item.Quantity > device.quantityForRent)) // Otra prueba
                {
                    ModelState.AddModelError("RentalItems", $"Error! Campos Modelo: '{item.NameModel}' Marca: '{item.Brand}' vacíos o se supera cantidad disponible");
                }
                else
                {
                    var rentDevice = new RentDevice
                    {
                        DeviceId = device.Id,
                        Price = device.priceForRent,
                        Quantity = item.Quantity,
                        RentalId = rental.Id
                    };
                    rental.RentDevices.Add(rentDevice);
                    item.priceForRent = device.priceForRent;
                }
            }

            //Precio total
            rental.TotalPrice = rental.RentDevices.Sum(ri => ri.Price * ri.Quantity * numDays);

            //Si hay problemas con la cantidad o que el dispositivo no existe...
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(rental);

            try
            {
                //we store in the database both rental and its rentalitems
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Rental", $"Error! There was an error while saving your rental, plese, try again later"); //Otra prueba
                return Conflict("Error" + ex.Message);

            }

            var rentalDetail = new RentalDetailDTO(rental.ApplicationUser.Name, rental.ApplicationUser.Surname, rental.DeliveryAddress, rental.RentalDate, rental.TotalPrice, rental.RentalDateFrom, rental.RentalDateTo, rentalForCreate.RentalItems);

            return CreatedAtAction("GetRentalDetail", new { id = rental.Id }, rentalDetail);

        }
    }
}