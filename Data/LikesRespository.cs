using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.Extensions;
using DaringAPI.Helpers;
using DaringAPI.interfaces;
using Microsoft.EntityFrameworkCore;

namespace DaringAPI.Data
{
    public class LikesRespository : ILikesRepository
    {
        private readonly DaringAppDbContext _context;
        public LikesRespository(DaringAppDbContext context)
        {
            this._context = context;
        }

        public async Task<Like> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, likedUserId);
        }

        // public async Task<IEnumerable<LikeDTO>> GetUserLikes(string predicate, int userId)
        // {
             public async Task<PagedList<LikeDTO>> GetUserLikes(LikesParams likesParams)
        {
           var users = _context.Users.OrderBy(u=>u.UserName).AsQueryable();
           var likes =_context.Likes.AsQueryable();
           if(likesParams.predicate=="liked"){
               likes=likes.Where(like=>like.SourceUserId==likesParams.UserId);
               users = likes.Select(like=>like.LikedUser);
           }
           if(likesParams.predicate=="likedBy"){
                likes=likes.Where(like=>like.LikedUserId==likesParams.UserId);
               users = likes.Select(like=>like.SourceUser);
           }
           var likedUsers = users.Select(user=>new LikeDTO{
                            UserName = user.UserName,
                            KnownAs = user.KnownAs,
                            Age = user.BirthDate.calculateAge(), 
                            PhotoUrl = user.Photos.FirstOrDefault(p=>p.IsMain).Url, 
                            City =user.City,
                            Id = user.Id

           });
           return await PagedList<LikeDTO>.CreateAsync(likedUsers, 
                                                       likesParams.PageNumber, 
                                                       likesParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                         .Include(u=>u.LikedUsers)
                         .FirstOrDefaultAsync(x=>x.Id==userId);
        }
    }
}