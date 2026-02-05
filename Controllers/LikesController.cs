using System.Collections.Generic;
using System.Threading.Tasks;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.Extensions;
using DaringAPI.Helpers;
using DaringAPI.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace DaringAPI.Controllers
{
    public class LikesController : BaseApiController
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IUserRepository _userRespository;
        public LikesController(ILikesRepository likesRepository,
        
        IUserRepository userRespository)
        {
            this._userRespository = userRespository;
            this._likesRepository = likesRepository;
        }
        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username){
            var mSourceUserId = User.GetUserId(); 
            var likedUser = await _userRespository.GetUserByUserNameAsync(username);
            var SourceUser = await _likesRepository.GetUserWithLikes(mSourceUserId);
            if(likedUser==null)
            return BadRequest("liked user not Found");
            if(SourceUser.UserName==username)
            return BadRequest("Self likes are boring!");
            var userLike = await _likesRepository.GetUserLike(mSourceUserId, likedUser.Id);
            if(userLike!=null)
            return BadRequest("You are already liked the Post!");
            userLike = new Like{
                SourceUserId = mSourceUserId,
                LikedUserId = likedUser.Id
            };
            SourceUser.LikedUsers.Add(userLike);
            if(await _userRespository.SaveAllAsync())
            return Ok();
            return BadRequest("Failed to Like the user!");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDTO>>> GetUserLikes([FromQuery]LikesParams likesParams){
            likesParams.UserId = User.GetUserId();
            var users =await _likesRepository.GetUserLikes(likesParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages );
            return Ok(users);

        }
}
}