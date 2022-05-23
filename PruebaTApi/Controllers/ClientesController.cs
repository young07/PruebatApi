using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PruebaTApi.Data;
using PruebaTApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PruebaTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesContext _context;

        public ClientesController(ClientesContext context)
        {
            _context = context;
        }

        [HttpGet("GetClientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesItems()
        {
            var clientes = await _context.Cliente.Include(t => t.Contactos).ToListAsync();
            return clientes;
        }

        [HttpGet("GetClienteDetails/{id}")]
        public async Task<ActionResult<Cliente>> GetClienteDetails(int id)
        {
            var Cliente = await _context.Cliente.Include(t => t.Contactos).FirstAsync(x => x.id == id);
            
            if(Cliente == null)
            {
                return NotFound();
            }
            return Cliente;
        }

        [HttpPost("SaveCliente")]
        public async Task<ActionResult<Cliente>> SaveCliente(Cliente item) {
            if (ModelState.IsValid)
            {
                _context.Cliente.Add(item);

                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetClienteDetails), new { id = item.id }, item);
            }

            return new Cliente();
        }

        [HttpPut("UpdateCliente/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, Cliente cliente)
        {
            if(id != cliente.id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteCliente/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            var direccion =  _context.Contacto.Where(x => x.cliente == cliente).ToList();

            if(cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            _context.Contacto.RemoveRange(direccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
