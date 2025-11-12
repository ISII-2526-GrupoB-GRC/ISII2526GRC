using System.Linq;
using System.Net;
using AppForSEII2526.API.DTOs.RepairDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepairsController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;

        //used to log any information when your system is running
        private readonly ILogger<RepairsController> _logger;

        public RepairsController(ApplicationDbContext context, ILogger<RepairsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<RepairDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetRepairs(string? name, string? scale)
        {
            if (_context.Repair == null)
            {
                _logger.LogError("Table Repair is null.");
                return NotFound();
            }

            var repairs = await _context.Repair
                .Where(r => (name == null || r.Name.Contains(name)) && (scale == null || r.Scale.Name.Contains(scale)))
                .Include(r => r.Scale)
                .Select(r => new RepairDTO(
                    r.Id,
                    r.Description,
                    r.Cost,
                    r.Name,
                    r.Scale.Name
                ))
                .ToListAsync();

            return Ok(repairs);

        }


    }
}