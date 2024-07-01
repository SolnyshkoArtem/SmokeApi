using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmokeApi.Model;

namespace SmokeApi
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Friend> Friends { get; set; } = null!;
        public DbSet<SmokeTime> SmokeTimes { get; set; } = null!;
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>()
            .HasOne(f => f.User)
            .WithMany(u => u.Friends)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendUser)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
