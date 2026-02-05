using System.Security.Claims;

namespace DaringAPI.Extensions
{
    public static class ClaimsPrincipleExtension
    {
        public static string GetUserName(this ClaimsPrincipal user){
        //    return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //ClaimTypes.Name representes unique name inside our token
         return user.FindFirst(ClaimTypes.Name)?.Value;
        
        }
        public static int GetUserId(this ClaimsPrincipal user){
      
         return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
         
        
        }
    }
}