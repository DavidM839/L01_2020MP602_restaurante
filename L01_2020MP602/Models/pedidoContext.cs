using Microsoft.EntityFrameworkCore;

namespace L01_2020MP602.Models
{
    public class pedidoContext : DbContext
    {
        public pedidoContext(DbContextOptions<pedidoContext> options) : base(options)
        {

        }
        public DbSet<pedidos> pedidos { get; set; }
    }
}