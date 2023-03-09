using DaringAPI.ClientUI.src.app.SignalR;
using DaringAPI.Data;
using DaringAPI.Helpers;
using DaringAPI.interfaces;
using DaringAPI.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DaringAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
       public static IServiceCollection AddApplicationServices(
                                   this IServiceCollection services, 
                                    IConfiguration config)
        {                              
           services.AddSingleton<AvailabilityTracker>();         
           services.Configure<CloudinarySettings>(config.GetSection("CloudinarySetting"));
           services.AddScoped<IToken, Token>();  
           services.AddScoped<IPhotoService,PhotoService>();
           services.AddScoped<UserActivityLogger>();
           services.AddScoped<ILikesRepository, LikesRespository>();
           services.AddScoped<IMessageRepository, MessageRepository>();
           services.AddScoped<IUserRepository, UserRepository>();

           services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
           services.AddDbContext<DaringAppDbContext>(options =>
            {
              //specify your connection stirng 
              //  options.UseSqlServer("connectionString");
              options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            return services;
            
      }
    }
}