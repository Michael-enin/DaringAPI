using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DaringAPI.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}