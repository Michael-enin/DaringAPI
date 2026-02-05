
using DaringAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DaringAPI.Data
{
    public class DaringAppDbContext : IdentityDbContext<AppUser, AppRole, int, 
                                                        IdentityUserClaim<int>, 
                                                        AppUserRole,
                                                        IdentityUserLogin<int>,
                                                        IdentityRoleClaim<int>,
                                                        IdentityUserToken<int>>
    {
        public DaringAppDbContext(DbContextOptions options)
        :base(options)
        {

        }
        // overding the identity users is not needed 
        // identityDbContext provides for us
        // public DbSet<AppUser> Users{get;set;}
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
            // for Identity user
            builder.Entity<AppUser>()
                .HasMany(u=>u.UserRoles)
                .WithOne(u=>u.User)
                .HasForeignKey(u=>u.UserId) 
                .IsRequired();
            //     // for Identity Role
            builder.Entity<AppRole>()
                .HasMany(u=>u.UserRoles)
                .WithOne(u=>u.Role)
                .HasForeignKey(u=>u.RoleId) 
                .IsRequired();
            
            
            builder.Entity<Like>()
                    .HasKey(k=>new{k.SourceUserId, k.LikedUserId});
                // //Source User
             builder.Entity<Like>()
                    .HasOne(one=>one.SourceUser)
                    .WithMany(mn=>mn.LikedUsers)
                    .HasForeignKey(k=>k.SourceUserId)
                    .OnDelete(DeleteBehavior.NoAction);

           //Liked User
             builder.Entity<Like>()
                    .HasOne(one=>one.LikedUser)
                    .WithMany(mn=>mn.LikedByUsers)
                    .HasForeignKey(k=>k.LikedUserId)
                    .OnDelete(DeleteBehavior.Cascade);
            //messages received
            builder.Entity<Message>()
                  .HasOne(u=>u.Recipient)
                  .WithMany(m=>m.MessagesRecieved)
                  .OnDelete(DeleteBehavior.Restrict);
           //messages sent
           builder.Entity<Message>()
                  .HasOne(u=>u.Sender)
                  .WithMany(m=>m.MessagesSent)
                  .OnDelete(DeleteBehavior.Restrict);


        }
    }
}