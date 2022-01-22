using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Entities;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {

        private readonly ApplicationContext _context;
        public QuestionsController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _context.Questions.ToListAsync();

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Question model)
        {
            if (model == null)
                return BadRequest();

            _context.Questions.Add(model);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
