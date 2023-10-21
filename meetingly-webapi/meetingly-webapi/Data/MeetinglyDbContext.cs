using meetingly_webapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace meetingly_webapi.Data
{
    public class MeetinglyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ScheduledDate> ScheduledDates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("MeetinglyDb");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.ScheduledDates)
                .WithOne()
                .HasForeignKey(sd => sd.UserId)
                .IsRequired();
        }
    }
}
