using System.Collections.Generic;
using System.Threading.Tasks;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.Helpers;

namespace DaringAPI.interfaces
{
    public interface IUserRepository
    {
         void Update(AppUser user);
         Task<bool> SaveAllAsync();
         Task<IEnumerable<AppUser>> GetUsersAsync(); 
         Task<AppUser> GetUserByIdAsync(int userId);
         Task<AppUser> GetUserByUserNameAsync(string userName);
        //  Task<IEnumerable<MemberDTO>> GetMembersAsync();
          Task<PagedList<MemberDTO>> GetMembersAsync(UserInputParams _params);
         
         Task <AppUserDTO> getUserFromDTOAsync(string username);
         Task <MemberDTO> GetMemberAsync(string username); 
        Task<IEnumerable<MemberDTO>> GetAllMembersAsync();
         
    }
}