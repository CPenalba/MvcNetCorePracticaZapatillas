using Microsoft.EntityFrameworkCore;
using MvcNetCorePracticaZapatillas.Models;

namespace MvcNetCorePracticaZapatillas.Data
{
    public class ZapatillaContext : DbContext
    {
        public ZapatillaContext(DbContextOptions<ZapatillaContext> options) : base(options) { }

        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<ImagenZapatilla> ImagenesZapatillas { get; set; }
    }
}
