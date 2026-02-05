using System.Text;

using DaringAPI.Controllers.Errors.MiddleWire;
using DaringAPI.Data;
using DaringAPI.Extensions;
using DaringAPI.interfaces;
using DaringAPI.services;
using DaringAPI.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DaringAPI
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;


        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //where dependency injections 
            //register Token service 
            services.AddApplicationServices(_config);         
            services.AddCors();    
            services.AddControllers();
            services.AddIdentityServices(_config);
            services.AddSignalR();
        }    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           // if (env.IsDevelopment())
            //  {
          //   app.UseDeveloperExceptionPage();
            //   //  app.UseSwagger();
            //    // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DaringAPI v1"));
          //  }
            app.UseMiddleware<ExceptionMiddleWire>();
          
            app.UseHttpsRedirection();
            app.UseRouting();
          
            app.UseCors(x =>x.AllowAnyHeader()
                             .AllowAnyMethod()
                             .AllowCredentials()
                             .WithOrigins("http://localhost:4200"));
            app.UseAuthentication();
            app.UseAuthorization();
    
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AvailableUserHub>("hubs/available");
                endpoints.MapHub<MessagesHub>("hubs/messages");
            });
        
        }
    }
}
