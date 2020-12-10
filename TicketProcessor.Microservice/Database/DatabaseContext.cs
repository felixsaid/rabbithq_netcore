using Microsoft.EntityFrameworkCore;
using Shared.Models.Models;

namespace TicketProcessor.Microservice.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
