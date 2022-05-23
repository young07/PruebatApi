using Microsoft.EntityFrameworkCore;
using PruebaTApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTApi.Data
{
    public class ClientesContext : DbContext
    {
        public ClientesContext(DbContextOptions<ClientesContext> options) : base(options) {}

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Contacto> Contacto { get; set; }
    }
}
