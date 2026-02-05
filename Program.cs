using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaringAPI.Data;
using DaringAPI.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DaringAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           // CreateHostBuilder(args).Build().Run();  initially like 
           var host = CreateHostBuilder(args).Build();
          using var scope = host.Services.CreateScope();
           var services = scope.ServiceProvider;
           // let's catch if any of the error is thrown 
           try{
               var context = services.GetRequiredService<DaringAppDbContext>();
               
               var userManager = services.GetRequiredService<UserManager<AppUser>>();
               var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

               await context.Database.MigrateAsync(); 
               await Seed.SeedUsers(userManager, roleManager);

           }
           catch(Exception x){
               var logger = services.GetRequiredService<ILogger<Program>>();
               logger.LogError(x, "UnExpected Error Occured during Migration");
           }
           await host.RunAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
