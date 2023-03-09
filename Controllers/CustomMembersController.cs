using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DaringAPI.Controllers
{
    public class CustomMembersController : BaseApiController
    {
        private readonly IUserRepository _userRespository;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public CustomMembersController(IUserRepository userRespository, 
                                  IPhotoService photoService, 
                                  IMapper mapper
                                  )
        {
            this._userRespository = userRespository;
            this._photoService = photoService;
            this._mapper = mapper;
        }
         [HttpGet]
         public async Task<ActionResult<IEnumerable<MemberDTO>>> getAllMembers(){
              var members = await _userRespository.GetAllMembersAsync();
              return Ok(members);
        // return Ok(members);
    
        
    }
    //     public  Task<ActionResult<IEnumerable<MemberDTO>>> getAllMembers(){
              
    //        throw new System.NotImplementedException();
        
    // }
    }
   
}