using System.Collections.Generic;
using System.Threading.Tasks;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.Helpers;

namespace DaringAPI.interfaces
{
    public interface ILikesRepository
    {
         Task<Like> GetUserLike(int sourceUserId, int likedUserId);
         Task<AppUser> GetUserWithLikes(int userId);
         //Task<IEnumerable<LikeDTO>> GetUserLikes(string predicae, int useId);
         Task<PagedList<LikeDTO>>  GetUserLikes(LikesParams likesParams);
    }
}