using Microsoft.EntityFrameworkCore;
using AirlineWeb.Models;

namespace AirlineWeb.Data
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> opt) : base(opt)
        {

        }
        public DbSet<WebhookSubscription> WebhookSubscriptions {get; set;}
        public DbSet<FlightDetail> FlightDetails { get; set; }
    }
}