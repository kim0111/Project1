using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Controllers.Helpers;
using Project.Models;
using Project.Models.Entities;
using System.Data;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        [EnableCors]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await _context.Users.ToListAsync();
            return Ok(response);
        }

        [EnableCors]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await _context.Users.FindAsync(id);

            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [EnableCors]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] User model)
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
            var model = await _context.Users.FindAsync(id);

            if (model == null)
                return NotFound();

            _context.Users.Remove(model);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [EnableCors]
        [HttpPost]
        [Route("excel")]
        public async Task<IActionResult> Excel(IFormFile excelFile)
        {
            if (excelFile == null)
                return BadRequest(new { errorText = "Invalid unput data" });

            try
            {
                var dtContent = FileHelpers.GetDataTableFromExcelLoadMode(excelFile);

                var user = new User();

                foreach(DataRow  dr in dtContent.Rows)
                {
                    user.FirstName = dr["FirsName"].ToString();
                    user.LastName = dr["LastName"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.RoleId = int.Parse(dr["RoleId"].ToString());

                    if (user.Password != null)
                        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    await _context.Users.AddAsync(user);

                    await _context.SaveChangesAsync();
                }
                return Ok();
            }
             catch
            {
                return BadRequest(new { errorText = "Posting data from  excel file has been failed!" });
            }
        }

        [EnableCors]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User model)
        {
            if (model == null)
                return BadRequest();

            _context.Users.Add(model);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
