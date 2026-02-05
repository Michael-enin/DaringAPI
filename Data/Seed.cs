using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DaringAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace DaringAPI.Data
{
    public class Seed
    {
       public static async Task SeedUsers(UserManager<AppUser> userManager, 
                                          RoleManager<AppRole> roleManager){
            //  public static async Task SeedUsers(UserManager<AppUser> userManager){
            if(await userManager.Users.AnyAsync()) 
            return ;
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var listOfUsers = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (listOfUsers == null) return ;
           
               var roles = new List<AppRole>
               {
                  new AppRole{Name = "Member"}, 
                  new AppRole{Name = "Admin"}, 
                  new AppRole{Name = "Moderator"}
               };
               foreach(var role in roles){
                   await roleManager.CreateAsync(role);
               }
               foreach(var user in listOfUsers ){
                /*
                  No need of using paassword hash as we use identity functionality
                  
                */
                 //using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                //user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@$$W0rd"));
                //user.PasswordSalt = hmac.Key;
                //passaword requires: number, upper case, lowercase, 
                //as we defined it in ApplicationSerciveExtension class password as
                //[opti.Password.RequireNonAlphanumeric=false;]

              await userManager.CreateAsync(user, "P@$$W0rd");
              await userManager.AddToRoleAsync(user, "Member");
             
            }
            // var admin = new AppUser{
            //   UserName = "admin"
            // };
               /* OR */
               var admin = new AppUser{
                     UserName = "admin"
                  };
                  await userManager.CreateAsync(admin, "P@$$W0rd");
                  // var adminList = new List<string>();
                  //     adminList.Add("Admin");
                  //     adminList.Add("Moderator");
                      
                  await userManager.AddToRolesAsync(admin, new []{"Admin", "Moderator" });

              //  await context.Users.AddAsync(user);
            }
              // usermanager takes care of saving database 
              //await context.SaveChangesAsync();
        
    }
}