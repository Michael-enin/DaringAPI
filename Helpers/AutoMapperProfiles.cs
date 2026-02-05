using System;
using System.Linq;
using AutoMapper;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.Extensions;

namespace DaringAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, AppUserDTO>()
            .ForMember(destination=>
                     destination.PhotoUrl, 
                     opt=>
                     opt.MapFrom(src =>src.Photos.FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(dest =>dest.Age, opt =>opt.MapFrom(src =>src.BirthDate.calculateAge()));
             CreateMap<Photo, PhotoDTO>();
             CreateMap<MemberUpdateDTO, AppUser>();
             CreateMap<RegisterDTO, AppUser>();
             CreateMap<AppUser, MemberDTO>();
             CreateMap<Message, MessageDTO>()
   /*get sender Photo*/ .ForMember(dest=>dest.SenderPhotoUrl, opt =>opt.MapFrom(src=>
                                   src.Sender.Photos.FirstOrDefault(p=>p.IsMain).Url))
 /*get recipient Photo*/.ForMember(dest=>dest.RecipientPhotoUrl, opt =>opt.MapFrom(src=>
                                   src.Recipient.Photos.FirstOrDefault(p=>p.IsMain).Url));
        CreateMap<Char, Byte>();
        }
    }
}