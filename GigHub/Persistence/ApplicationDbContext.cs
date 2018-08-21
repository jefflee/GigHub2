using System.Data.Entity;
using GigHub.Core.Models;
using GigHub.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Need to set your model here, then EF will know it when it do code first migration.
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Move this to GigConfiguration
            //modelBuilder.Entity<Gig>()
            //    .Property(g => g.ArtistId)
            //    .IsRequired();

            //modelBuilder.Entity<Gig>()
            //    .Property(g => g.Venue)
            //    .IsRequired()
            //    .HasMaxLength(255);

            //modelBuilder.Entity<Gig>()
            //    .Property(g => g.GenreId)
            //    .IsRequired();


            //Move this to GigConfiguration
            //modelBuilder.Entity<Attendance>()
            //    .HasRequired(a => a.Gig)
            //    .WithMany(g => g.Attendances)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Configurations.Add(new GigConfiguration());

            //Move to ApplicationUserConfiguration
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany( u => u.Followers)
            //    .WithRequired( f => f.Followee)
            //    .WillCascadeOnDelete(false);

            //Move to ApplicationUserConfiguration
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(u => u.Followees)
            //    .WithRequired(f => f.Follower)
            //    .WillCascadeOnDelete(false);

            //Move to UserNotificationConfiguration
            //modelBuilder.Entity<UserNotification>()
            //    .HasRequired(n => n.User)
            //    .WithMany(u => u.UserNotifications)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new AttendanceConfiguration());
            modelBuilder.Configurations.Add(new FollowingConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new UserNotificationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}