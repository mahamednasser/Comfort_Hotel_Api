using Comfort_Hotel_Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Comfort_Hotel_Api.Data
{
    public class AppDBContext:IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }
        public DbSet<Room> Rooms { get; set; }
       

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<RoomsRate> RoomsRates { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }





    }
}
