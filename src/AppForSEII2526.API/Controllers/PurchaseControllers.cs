using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AppForSEII2526.API.DTOs.PurchaseDTOs;
using Humanizer;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using static AppForSEII2526.API.Models.PaymentMethodTypes;


namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

        //Se usa para permitir que tu controlador acceda a la base de datos.
        private readonly ApplicationDbContext _context;
        //Se usa para registrar cualquier información mientras tu sistema está en funcionamiento.
        private readonly ILogger<PurchaseController> _logger;

        public PurchaseController(ApplicationDbContext context, ILogger<PurchaseController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /*[HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<PurchaseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDevices_TwoDates_DTO()
        {
            var Devices = await _context.Device
                .Select(c=>new PurchaseDTO(c.Name,c.Brand, c.Model,c.Color,c.priceForPurchace)).ToListAsync();
            return Ok(Devices);
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<PurchaseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetPurchase_COLOUR_FILTER_DTO(string? colourFilter)
        { 
            var devices = await _context.Device
                .Where(c=> c.Color.Contains(colourFilter) || (colourFilter==null))
                .Select(c => new PurchaseDTO(c.Name, c.Brand,c.Model,c.Color,c.priceForPurchace)).ToListAsync();
            return Ok(devices);


        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<PurchaseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetPurchase_NAME_FILTER_DTO(string? nameFilter) { 

            var devices = await _context.Device
                .Where(c=> c.Name.Contains(nameFilter) || (nameFilter == null))
                .Select(c=> new PurchaseDTO(c.Name,c.Brand, c.Model, c.Color, c.priceForPurchace)).ToListAsync();
            return Ok(devices);


        }


    */





       



        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(PurchaseDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreatePurchase(PurchaseForCreateDTO purchaseForCreate)
        {

            if (purchaseForCreate.purchaseItems.Count == 0)//
            {
                ModelState.AddModelError("purchaseItems", "ERROR! You must buy at least one item");


            }
            if (purchaseForCreate.deliveryAddress == null || purchaseForCreate.deliveryAddress == "") //
            {
                ModelState.AddModelError("deliveryAddress", "ERROR! You must put a delivery addres");
            }


            var user = _context.ApplicationUser.FirstOrDefault(u => u.Name == purchaseForCreate.name);
            if (user == null)
            { //
                ModelState.AddModelError("PurchaseApplicationUser", "Error!. UserName is not registered");
            }

            var devicesNamesModels = purchaseForCreate.purchaseItems.Select(pi => pi.nameModel).ToList<string>();

            var devices = _context.Device.Include(m => m.PurchaseItems)
                .ThenInclude(pi => pi.Purchase)
                .Where(d => devicesNamesModels.Contains(d.Model.NameModel))

                .Select(m => new
                {
                    m.Id,
                    m.Brand,
                    m.Color,
                    m.priceForPurchace,
                    m.quantityForPurchase,
                    m.Model.NameModel








                })
                .ToList();//atasco preguntar en clase.

            //Creo la compra
            Purchase purchase = new Purchase(

                purchaseForCreate.deliveryAddress,
                //purchaseForCreate.id,
                purchaseForCreate.paymentMethod,
                DateTime.Now,
                0, // TotalPrice inicializado a 0 cambiar
                0, // TotalQuanty inicializado a 0 cambiar
                new List<PurchaseItem>(),// Instancia concreta de la lista
                user//user
            );

            foreach (var item in purchaseForCreate.purchaseItems)
            {
                var device = devices.FirstOrDefault(d => d.NameModel == item.nameModel);


                if ((device == null))
                {
                    ModelState.AddModelError("purchaseItems", $"ERROR! The device {item.nameModel} is not available");

                }
                else
                {
                    string description = item.description;
                    int quantity = item.quantity;
                    int deviceId = device.Id;
                    purchase.PurchaseItems.Add(new PurchaseItem(description, quantity, device.Id, purchase));
                    item.price = device.priceForPurchace;


                }

            }
            purchase.TotalPrice = purchaseForCreate.purchaseItems.Sum(pi => pi.price * pi.quantity);
            purchase.TotalQuanty = purchaseForCreate.purchaseItems.Sum(pi => pi.quantity);



            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(purchase);


            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Purchase", $"Error! There was an error while saving your puchase, please ty again later");
                return Conflict("Error" + ex.Message);



            }



            var purchaseDetail = new PurchaseDetailDTO(
                purchase.Id,
                purchase.ApplicationUser.Name,
                purchase.ApplicationUser.Surname,
                purchase.DeliveryAddress,
                purchase.PurchaseDate,
                purchase.TotalPrice,
                purchase.TotalQuanty,
                purchaseForCreate.purchaseItems


                );

            return CreatedAtAction("Get_Purchase_Detail_DTO", new { id = purchase.Id }, purchaseDetail);

        }
    }
}
