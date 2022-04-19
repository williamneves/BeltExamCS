using Microsoft.EntityFrameworkCore;

namespace DojoMeetUP.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meet> Meets { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}