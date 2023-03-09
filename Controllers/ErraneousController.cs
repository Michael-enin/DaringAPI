using System;
using DaringAPI.Data;
using DaringAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaringAPI.Controllers
{
    public class ErraneousController : BaseApiController
    {
        private readonly DaringAppDbContext _context;
        public ErraneousController(DaringAppDbContext context)
        {
            this._context = context;
        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult <string> getSecretMessage()
        {
            return "secret Message";
        }

            [HttpGet("not-found")]   
     public ActionResult<AppUser> getNotFound(){
                var result = _context.Users.Find(-1);
                if(result==null)
                return NotFound();
                else
                return Ok(result);

        }
        
         //this is error is from developer we get from server 
        [HttpGet("server-error")]
        public ActionResult<string> getServerError(){
            // try{
                var result = _context.Users.Find(-1);
                var er = result.ToString();
                return er;

            // }
            // catch (Exception)
            // {
            //   return  StatusCode(500, "Not able to Handle in Your computer");
            // }
            
        }
        [HttpGet("bad-request")]
        public ActionResult<string> getBadRequest(){

            return BadRequest("This is really bad request");
        }
    }
}