using System;
using System.Collections.Generic;
using DaringAPI.Extensions;
using Microsoft.AspNetCore.Identity;

namespace DaringAPI.Entities
{
    public class AppUser : IdentityUser<int>
    {
              /* these three properties are already defined in IdentityUser*/
        // public int Id { get; set; }
        // public string UserName { get; set; }
        // public byte[] PasswordHash { get; set; }
             /* IdentityUser does not use PasswordSalt*/
         public byte[] PasswordSalt { get; set; }

        public DateTime BirthDate { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Like> LikedByUsers { get; set; }
        public ICollection<Like> LikedUsers { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesRecieved { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
      
         public int GetAge(){
            
            return BirthDate.calculateAge();
        
        }
    }

   
}