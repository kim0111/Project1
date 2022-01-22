using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Entities;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public RolesController(ApplicationContext context)
        {
            _context = context;
        }

        [EnableCors]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await _context.Roles.ToListAsync();
            return Ok(response);
        }

        [EnableCors]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await _context.Roles.FindAsync(id);

            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [EnableCors]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Role model)
        {
            if (model == null)
                return BadRequest();

            _context.Roles.Add(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [EnableCors]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Role model)
        {
            _context.Update(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [EnableCors]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Roles.FindAsync(id);

            if (model == null)
                return NotFound();

            _context.Roles.Remove(model);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
