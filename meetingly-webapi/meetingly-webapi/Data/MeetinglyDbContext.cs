using meetingly_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace meetingly_webapi.Data
{
    public class MeetinglyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ScheduledDate> ScheduledDates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("MeetinglyDb");
    }
}
