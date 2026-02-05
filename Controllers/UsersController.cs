using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DaringAPI.Data;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.Extensions;
using DaringAPI.Helpers;
using DaringAPI.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using System.Collections;
///using System.Collections.Generic;
namespace DaringAPI.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        // private readonly DaringAppDbContext _context;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository repository,
                               IMapper mapper,
                               IPhotoService photoService)
        {
            this._photoService = photoService;
            this._mapper = mapper;
            this._repository = repository;
        }

        // public UsersController(DaringAppDbContext context)
        // {
        //     this._context = context;
        // }

        // [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers([FromQuery]UserInputParams _params)
        {
            var mUser = await _repository.GetUserByUserNameAsync(User.GetUserName());

               _params.CurrentUserName = mUser.UserName;
            if(string.IsNullOrEmpty(_params.Gender)){
                _params.Gender = mUser.Gender=="male" ? "female" : "male";
            }
            var users = await _repository.GetMembersAsync(_params);
            //   var   = _mapper.Map<IEnumerable<AppUserDTO>>(users);
            Response.AddPaginationHeader(users.CurrentPage, 
                                         users.PageSize, 
                                         users.TotalCount, 
                                         users.TotalPages);

            return Ok(users);
        }
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers(){
        //     var users = await _repository.GetUsersAsync();
        //     return Ok(users);
        // }
        // [Authorize]
        // [HttpGet("{id}")]
        // public async Task<ActionResult<AppUserDTO>> getUser(int id)
        // {
        //     var user = await _repository.GetUserByIdAsync(id);
        //     return _mapper.Map<AppUserDTO>(user);
        // }
        // [Authorize(Roles ="Member")]
        [HttpGet("{username}", Name ="GetUser")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
            return await _repository.GetMemberAsync(username);

        }
        [HttpPut]
        public async Task<ActionResult> updateUser(MemberUpdateDTO memberUpdateDTO)
        {
            // var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //   var username=User.GetUserName();
            var user = await _repository.GetUserByUserNameAsync(User.GetUserName());
            _mapper.Map(memberUpdateDTO, user);
            _repository.Update(user);
            if (await _repository.SaveAllAsync())
                return NoContent();
            return BadRequest("Failed to update user");

        }
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
        {
            var user = await _repository.GetUserByUserNameAsync(User.GetUserName());
            var result = await _photoService.AddPhotoAsync(file);
            if(result.Error!=null)
            return BadRequest(result.Error.Message);
            var photo = new Photo
            {
              Url = result.SecureUrl.AbsoluteUri, 
              PublicId = result.PublicId 

            };             
            if(user.Photos.Count ==0){
              //  photo.IsMain=true;
              //in logic it id false;
                photo.IsMain=false;
            }
             user.Photos.Add(photo);
            if(await _repository.SaveAllAsync()){
               // return _mapper.Map<PhotoDTO>(photo);
               //  return CreatedAtRoute("GetUser", _mapper.Map<PhotoDTO>(photo));
               return CreatedAtRoute("GetUser", new {username = user.UserName},_mapper.Map<PhotoDTO>(photo));
            }
                
                return BadRequest("Problem Occured in PhotoUpload"); 

        }
        [HttpPut ("set-profile-photo/{photoId}")]
        public async Task<ActionResult> setProfilePhoto(int photoId){
            var user = await _repository.GetUserByUserNameAsync(User.GetUserName());
            var photo = user.Photos.FirstOrDefault(x=>x.Id==photoId);
            if(photo.IsMain==false)
            return BadRequest("This is already your Profile Photo");
            var current = user.Photos.FirstOrDefault(x=>x.IsMain);
            if(current!=null)
        current.IsMain=false;
        photo.IsMain=true ;
        if(await _repository.SaveAllAsync())
        return NoContent();

    return BadRequest("Failed to set Profile Photo");
        }
    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int photoId){
    var user = await _repository.GetUserByUserNameAsync(User.GetUserName());
    var photo = user.Photos.FirstOrDefault(x =>x.Id==photoId);
    if(photo ==null)
        return NotFound();
    if(photo.IsMain)
        return BadRequest("you cannot delete Profile Photo");
    if(photo.PublicId !=null){
        var result = await _photoService.DeletePhotoAsync(photo.PublicId);
        if(result.Error!=null)
        return BadRequest(result.Error.Message);
    }   
    user.Photos.Remove(photo);
    if(await _repository.SaveAllAsync())
    return Ok();
    return BadRequest("Failed to delete!");
    }
  }
}