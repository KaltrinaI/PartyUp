using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PartyUp.Models;

namespace PartyUp.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BusinessEntity> BusinessEntities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ReservationRequest> ReservationRequests { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BusinessEntity>()
                .HasMany(be => be.Events)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Location)
                .WithMany()
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.ReservationRequests)
                .WithOne(rr => rr.Event)
                .HasForeignKey(rr => rr.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Tags)
                .WithMany(t => t.Events);

            modelBuilder.Entity<ReservationRequest>()
             .HasOne(rr => rr.User)
             .WithMany(u => u.ReservationRequests)
             .HasForeignKey(rr => rr.UserId);

            modelBuilder.Entity<ReservationRequest>()
                .HasOne(rr => rr.Event)
                .WithMany(e => e.ReservationRequests)
                .HasForeignKey(rr => rr.EventId);

            modelBuilder.Entity<Location>()
                .HasKey(l => l.LocationId);

            modelBuilder.Entity<Event>()
                .Property(e => e.DateTimeOfEvent)
                .HasColumnType("timestamptz");
        }
    }
}
