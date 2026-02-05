using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DaringAPI.Data;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DaringAPI.Controllers
{
    public class AccountController : BaseApiController
    {
     //   private readonly DaringAppDbContext _context;
        private readonly IToken _token;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                  IToken token, IMapper mapper)

        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._token = token;

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.UserName))
                return BadRequest("The UserName Exists in Our Database");
            var user = _mapper.Map<AppUser>(registerDTO);
            // using var hmac = new HMACSHA512();
            // var user = new AppUser
            // {
            //     Name = registerDTO.UserName.ToLower(),
            //     KnownAs = registerDTO.KnownAs,
            //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            //     PasswordSalt = hmac.Key
            // };
            user.UserName = registerDTO.UserName.ToLower();
            // As I use identityUser no need of passwordhash and passwordsalt
            // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
            // user.PasswordSalt = hmac.Key;   

            // _context.Users.Add(user);
            // await _context.SaveChangesAsync();
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if(!result.Succeeded)
               return BadRequest(result.Errors);
            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if(!roleResult.Succeeded) 
               return BadRequest(roleResult.Errors);
            if(!result.Succeeded) return BadRequest(result.Errors);
            return new UserDTO
            {
                UserName = user.UserName,
                Token =await _token.createToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> login(LoginDTO loginDTO)
        {
            //wherer x=>x.Name is attribute of AppUser Entity
            //alternatevely you can use .FirstOrDefualtAsync() method
            //but SingleOrDefault() throws error
            // var user = await _context.Users
            var user = await _userManager.Users
                   .Include(p => p.Photos)
                   .SingleOrDefaultAsync(x => x.UserName == loginDTO.username);
            if (user == null) 
               return Unauthorized("Invalid User");
         //   var roleResult = await _userManager.AddToRoleAsync(user, "Member");
              // false roleResult;
         //   if(!roleResult.Succeeded) return BadRequest(roleResult.Errors);
            // using var hmac = new HMACSHA512(user.PasswordSalt);
            // var calculatedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));
            // for (int i = 0; i < calculatedHash.Length; i++)
            // {
            //     if (calculatedHash[i] != user.PasswordHash[i])
            //         return Unauthorized("Invalid Password");

            // }
            var result = await _signInManager
                               .CheckPasswordSignInAsync(user, loginDTO.password, false);
            if(!result.Succeeded) Unauthorized();

            return new UserDTO
                {
                    UserName = user.UserName,
                    Token = await _token.createToken(user),
                    //? is to mean that the Photo url (would be) null
                    PhotoUrl = user.Photos.FirstOrDefault()?.Url,
                    KnownAs = user.KnownAs,
                    Gender = user.Gender

                };    
        }
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users
                                           .AnyAsync(usr => usr.UserName == username.ToLower());
        }
    }
}