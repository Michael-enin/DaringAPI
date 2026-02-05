using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.Helpers;
using DaringAPI.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DaringAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DaringAppDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DaringAppDbContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<IEnumerable<MemberDTO>> GetAllMembersAsync()
        {
            return await _context.Users
                     .Include(p =>p.Photos)
                     .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                     .ToListAsync();
        }

        public async Task<MemberDTO> GetMemberAsync(string username)
        {
            return await _context.Users.Where(x=>x.UserName==username)
                                       .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                                       .SingleOrDefaultAsync();
                                       
        }

        public async Task<PagedList<MemberDTO>> GetMembersAsync(UserInputParams _params)
        {
            // var query = _context.Users.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
            //                           .AsNoTracking()
            //                           .AsQueryable();// load data from database and then filtering 
                 
           var query = _context.Users.AsQueryable();
                   //  /\filter by gender/\
               query = query.Where(u=>u.UserName!=_params.CurrentUserName);
               query =query.Where(u=>u.Gender==_params.Gender);
               // /\ filter by age /\
               var minBirthDate = DateTime.Today.AddYears( - _params.MaxAge-1);
               var maxBirthDate = DateTime.Today.AddYears(-_params.MinAge);
               // /\sorting /\
               query = _params.OrderBy switch{
                   "created"=>query.OrderBy(u=>u.Created),
                   // _ /inderscore is the way we specifiy the default in this switch in C# 8
                    _ =>query.OrderByDescending(u=>u.LastActive)
               };
               query = query.Where(u=>u.BirthDate >= minBirthDate && u.BirthDate <= maxBirthDate);

     //IQueryable<MemberDTO> src = query.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).AsNoTracking();
              return await PagedList<MemberDTO>.CreateAsync(query.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).AsNoTracking(), _params.PageNumber, _params.PageSize);
              
        }

      

        public async Task<AppUser> GetUserByIdAsync(int userId)
        {
            // throw new System.NotImplementedException();
            return await _context.Users.FindAsync(userId);
        }

        public async Task<AppUser> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == userName);
            // throw new System.NotImplementedException();
        }

     

        public async Task<AppUserDTO> getUserFromDTOAsync(string username)
        {
            return await _context.Users
                  .Where(x => x.UserName == username)
                  .ProjectTo<AppUserDTO>(_mapper.ConfigurationProvider)
                  .SingleOrDefaultAsync();
            // throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
            // throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AppUserDTO>> getUsersFromDTOAsync()
        {
            return await _context.Users
                  .ProjectTo<AppUserDTO>(_mapper.ConfigurationProvider)
                  .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            int isSaved = await _context.SaveChangesAsync();
            if (isSaved > 0)
                return true;
            else
                return false;
            // throw new System.NotImplementedException();
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            // throw new System.NotImplementedException();
        }

       
    }
}