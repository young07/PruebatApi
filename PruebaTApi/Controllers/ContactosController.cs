using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTApi.Data;
using PruebaTApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTApi.Controllers
{
    public class ContactosController : Controller
    {
        private readonly ClientesContext _context;

        public ContactosController(ClientesContext context)
        {
            _context = context;
        }
        [HttpGet("GetContactos")]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetContactos(int clienteid)
        {
            var cliente = _context.Cliente.Find(clienteid);
            return await  _context.Contacto.Where(x => x.cliente == cliente).ToListAsync();
        }

        [HttpPost("SaveContacto")]
        public async Task<ActionResult<Cliente>> SaveGetContacto(int clienteId, string contacto)
        {
            if (clienteId == 0 || contacto == "")
            {
                return BadRequest();
            }
            var cliente = _context.Cliente.Find(clienteId);
            if (cliente == null)
            {
                return BadRequest();
            }
            Contacto direcion = new Contacto();
            direcion.Numero = contacto;
            direcion.cliente = cliente;
            _context.Contacto.Add(direcion);

            await _context.SaveChangesAsync();
            return NoContent();      
        }

        [HttpPut("UpdateContacto/{id}")]
        public async Task<IActionResult> UpdateContacto(int id, string descripcion)
        {

            if (id == 0 && descripcion == "")
            {
                return BadRequest();
            }

            Contacto contacto = await _context.Contacto.FindAsync(id);

            contacto.Numero = descripcion;

            _context.Entry(contacto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("DeleteContactos/{id}")]
        public async Task<IActionResult> EliminarContactos(int id)
        {
            var contacto = _context.Contacto.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }

            _context.Contacto.RemoveRange(contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
