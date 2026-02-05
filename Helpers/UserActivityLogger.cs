using System;
using System.Threading.Tasks;
using DaringAPI.Extensions;
using DaringAPI.interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DaringAPI.Helpers
{
    public class UserActivityLogger : IAsyncActionFilter
    {
        public  async Task OnActionExecutionAsync(ActionExecutingContext context,
                                            ActionExecutionDelegate next)
        {
           var resulContext = await next();
           if(!resulContext.HttpContext.User.Identity.IsAuthenticated){
               return;
           }
               else{
                    // var userName = resulContext.HttpContext.User.GetUserName();
               var userId = resulContext.HttpContext.User.GetUserId();
               var repo = resulContext.HttpContext.RequestServices.GetService<IUserRepository>();
            //   var user = await repo.GetUserByUserNameAsync(userName);
               var user= await repo.GetUserByIdAsync(userId);
               user.LastActive = DateTime.Now;
               await repo.SaveAllAsync();}
        }
    }
}