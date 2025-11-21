using AppForSEII2526.API.DTOs.ReceiptDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        //used to enable your controller to access to the database 
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<ReceiptsController> _logger;

        public ReceiptsController(ApplicationDbContext context, ILogger<ReceiptsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReceiptDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetReceipt(int id)
        {
            if (_context.Receipt == null)
            {
                _logger.LogError("Error: Receipts table does not exist");
                return NotFound();
            }
            var receipt = await _context.Receipt
                .Where(r => r.Id == id)
                    .Include(r => r.ApplicationUser)
                    .Include(r => r.ReceiptItems)
                        .ThenInclude(ri => ri.Repair)
                            .ThenInclude(rep => rep.Scale)
                .Select(r => new ReceiptDetailDTO(
                    r.Id, r.ApplicationUser.Name, r.ApplicationUser.Surname, r.deliveryAddres, r.ReceiptDate, r.TotalPrice
                    , r.ReceiptItems.Select(ri => new ReceiptItemDTO(
                        ri.Repair.Name,
                        ri.Repair.Scale.Name,
                        ri.Repair.Cost,
                        ri.Model
                    )).ToList<ReceiptItemDTO>()
                ))
                .FirstOrDefaultAsync();
            if (receipt == null)
            {
                _logger.LogWarning($"Error: Rental with id {id} does not exist");
                return NotFound();
            }
            return Ok(receipt);
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReceiptDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReceipt(ReceiptForCreateDTO receiptForCreateDTO)
        {
            // Validaciones antes de continuar con otras operaciones
            if (receiptForCreateDTO.receiptItems == null || receiptForCreateDTO.receiptItems.Count == 0)
            {
                ModelState.AddModelError("ReceiptItems", "At least one receipt item is required.");
            }

            if (receiptForCreateDTO.userdeliveryaddress != null && !(receiptForCreateDTO.userdeliveryaddress.Contains("Calle") || receiptForCreateDTO.userdeliveryaddress.Contains("Avenida"))) {
                ModelState.AddModelError("UserDeliveryAddres", "Error en la dirección de envío. Por favor, introduce una dirección válida inluyendo las palabras Calle o Avenida");
            }

            var user = await _context.ApplicationUser.FirstOrDefaultAsync(u => u.UserName == receiptForCreateDTO.username);
            if (user == null)
            {
                ModelState.AddModelError("User", "The specified user does not exist.");
            }

            // Si hay errores de validación, devolver un BadRequest con los detalles
            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            // Extraer los nombres de las reparaciones para comprobar si existen en la base de datos
            var repairNames = receiptForCreateDTO.receiptItems.Select(ri => ri.Name).ToList();
            var repairs = await _context.Repair
                .Include(r => r.Scale)
                .Where(r => repairNames.Contains(r.Name))
                .ToListAsync();

            // Crear el receipt         public Receipt(PaymentMethodTypes paymentMethod, DateTime receiptDate, IList<ReceiptItem> receiptItems, ApplicationUser applicationUser, string deliveryAddres)

            Receipt receipt = new Receipt(receiptForCreateDTO.paymentMethod, DateTime.Now, new List<ReceiptItem>(), user, receiptForCreateDTO.userdeliveryaddress);


            // Crear los receipt items y añadirlos al receipt
            foreach (var itemDTO in receiptForCreateDTO.receiptItems)
            {
                var repair = repairs.FirstOrDefault(r => r.Name == itemDTO.Name);
                if (repair == null)
                {
                    ModelState.AddModelError("ReceiptItems", $"The repair '{itemDTO.Name}' does not exist.");
                }
                else
                {
                    receipt.ReceiptItems.Add(new ReceiptItem(itemDTO.Model, repair, receipt));
                    // Calcular el precio total
                    receipt.TotalPrice = receipt.TotalPrice + itemDTO.Cost;
                }
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            _context.Add(receipt);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new receipt.");
                return Conflict("An error occurred while creating the receipt. Please try again.");
            }

            // Devolver el receipt creado
            var receiptDetail = new ReceiptDetailDTO(
                receipt.Id,
                receipt.ApplicationUser.Name,
                receipt.ApplicationUser.Surname,
                receipt.deliveryAddres,
                receipt.ReceiptDate,
                receipt.TotalPrice,
                receipt.ReceiptItems.Select(ri => new ReceiptItemDTO(
                    ri.Repair.Name,
                    ri.Repair.Scale.Name,
                    ri.Repair.Cost,
                    ri.Model
                )).ToList()
            );

            return CreatedAtAction("GetReceipt", new { id = receipt.Id }, receiptDetail);
        }
    }
}
