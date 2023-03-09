using System.Linq;
using System.Threading.Tasks;
using DaringAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DaringAPI.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                              .Include(u => u.UserRoles)
                              .ThenInclude(u=>u.Role)
                              .OrderBy(u => u.UserName)
                              .Select(u =>new {
                                   u.Id, 
                                   UserName = u.UserName,
                                   Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                                       })
                              .ToListAsync();
                return Ok(users);
        }
        [Authorize(Policy ="RequiredAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles){
                    var selectedRoles = roles.Split(",").ToArray();
                    var user = await _userManager.FindByNameAsync(username);
                    if(user == null) return NotFound("Could Not Found!");
                    var userRoles =await _userManager.GetRolesAsync(user);
                    var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
                    if(!result.Succeeded) return BadRequest("Failed To Add To Roles");
                    result = await _userManager.RemoveFromRolesAsync(user, selectedRoles.Except(userRoles));
                    if(!result.Succeeded) return BadRequest("Failed To Remove From Roles");
                    return Ok(await _userManager.GetRolesAsync(user));
        }
        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public ActionResult GetPhotosForModeration()
        {
            return Ok("Admins or Moderates can access this resource");
        }
    }
}