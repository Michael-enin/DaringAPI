

using System.Text;
using System.Threading.Tasks;
using DaringAPI.Data;
using DaringAPI.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DaringAPI.Extensions
{
    public static class IdentityServiceExtensions
    {
     public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config){
                    
                    //extension for identity
                    services.AddIdentityCore<AppUser>(opti=>{
                            opti.Password.RequireNonAlphanumeric=false;})
                      .AddRoles<AppRole>()
                      .AddRoleManager<RoleManager<AppRole>>()
                      .AddSignInManager<SignInManager<AppUser>>()
                      .AddRoleValidator<RoleValidator<AppRole>>()
                      .AddEntityFrameworkStores<DaringAppDbContext>();

                    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                                  {
                                    ValidateIssuerSigningKey = true, 
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                                    ValidateIssuer=false,
                                   ValidateAudience=false,    
                                };
                                options.Events = new JwtBearerEvents
                                {
                                   OnMessageReceived = context =>
                                   {
                                      var accessToken = context.Request.Query["access_token"];
                                      var path = context.HttpContext.Request.Path;
                                      if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs")){
                                         context.Token = accessToken;
                                      }
                                      return Task.CompletedTask;
                                   }
                                };
                     }); 
                     services.AddAuthorization(opt =>
                                             { 
                                                opt.AddPolicy("RequiredAdminRole", plcy =>plcy.RequireRole("Admin")); 
                                                opt.AddPolicy("ModeratePhotoRole", plcy =>plcy.RequireRole("Admin", "Moderator"));
                                             });                         
                     
                return services;     
     }
    }
}