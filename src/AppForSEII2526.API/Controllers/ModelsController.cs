using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    public class ModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ILogger _logger;

        private ModelsController(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get: api/Devices/GetDevicesForRental
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetModelsForRental(string? modelName) // ? significa que es opcional el parámetro
        {
            var models = await _context.Modelo
                .Where(model => (modelName == null || model.NameModel.Contains(modelName)))
                .OrderBy(model => model.NameModel)
                .Select(model => model.NameModel)
                .ToListAsync();
            return Ok(models);
        }
    }
}
