using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Answers_API_LeonardoCortes.Models;
using Answers_API_LeonardoCortes.Models.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Answers_API_LeonardoCortes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AnswersDBContext _context;

        public UsersController(AnswersDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/GetUserInfo?id=3'
        [HttpGet("GetUserInfo")]
        public ActionResult<IEnumerable<UserDTO>> GetUserInfo(int id)
        {
            var query = (from u in _context.Users
                         join r in _context.UserRoles on u.UserRoleId equals r.UserRoleId
                         join c in _context.Countries on u.CountryId equals c.CountryId
                         where u.UserId == id
                         select new
                         {
                             idusuario = u.UserId,
                             nombreusuario = u.UserName,
                             nombre = u.FirstName,
                             apellido = u.LastName,
                             numerotelefono = u.PhoneNumber,
                             cantidadstrike = u.StrikeCount,
                             correorrespaldo = u.BackUpEmail,
                             descripciontrabajo = u.JobDescription,
                             idestatususuario = u.UserStatusId,
                             idpais = c.CountryId,
                             idrol = r.UserRoleId
                         }).ToList();

            List<UserDTO> list = new List<UserDTO>();

            foreach (var item in query)
            {
                UserDTO NewItem = new UserDTO();

                NewItem.IDUsuario = item.idusuario;
                NewItem.NombreUsuario = item.nombreusuario;
                NewItem.Nombre = item.nombre;
                NewItem.Apellido = item.apellido;
                NewItem.NumeroTelefono = item.numerotelefono;
                NewItem.CantidadStrike = item.cantidadstrike;
                NewItem.CorreoRespaldo = item.correorrespaldo;
                NewItem.DescripcionTrabajo = item.descripciontrabajo;
                NewItem.IDPais = item.idpais;
                NewItem.IDRol = item.idrol;
                list.Add(NewItem);
            }

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }


// GET: api/Users/5
[HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
