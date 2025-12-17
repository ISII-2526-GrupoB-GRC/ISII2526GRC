using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ModelsController> _logger;

        public ModelsController(ApplicationDbContext context, ILogger<ModelsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get: api/Models/GetModelsForRental
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
