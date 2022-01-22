using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Models.Entities;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonsController : ControllerBase
    {

        private readonly ApplicationContext _context;
        public CommonsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Common model)
        {
            if (model == null)
                return BadRequest();

            _context.Commons.Add(model);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
