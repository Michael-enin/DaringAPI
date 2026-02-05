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
using Microsoft.EntityFrameworkCore;

namespace DaringAPI.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DaringAppDbContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DaringAppDbContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
           // var message = await _context.Messages
           return await _context.Messages
            .Include(p=>p.Sender)
            .Include(p=>p.Recipient)
            .SingleOrDefaultAsync(x=>x.Id==id);
          //  return message;
        }

        public async Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams)
        {
            // throw new System.NotImplementedException();
            //(late message/with long time) comes first
            var query = _context.Messages.OrderByDescending(m => m.MessageSentTime)
                            .AsQueryable();
            query = messageParams.Container switch
            {
                   "Inbox" => query.Where(u => u.Sender.UserName == messageParams.UserName
                                          && u.RecipientDeleted == false),
             "Outbox" => query.Where(u => u.Recipient.UserName == messageParams.UserName 
                                           && u.SenderDeleted == false),
                 _ => query.Where(u => u.Recipient.UserName == messageParams.UserName &&
                                        u.RecipientDeleted == false && 
                                                u.DateRead == null)
            };
            var messages = query.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider);
        return await PagedList<MessageDTO>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUserName, 
                                                                     string recipientName)
        {
            var messages = await _context.Messages
                            .Include(n=>n.Sender).ThenInclude(p=>p.Photos)
                            .Include(n=>n.Recipient).ThenInclude(p=>p.Photos)
                            .Where(m=>m.Recipient.UserName==currentUserName && 
                                       m.RecipientDeleted ==false &&
                                       m.Sender.UserName==recipientName ||                                    
                                      m.Recipient.UserName==recipientName &&
                                      m.Sender.UserName==currentUserName && m.SenderDeleted==false)
                                    .OrderBy(m=>m.MessageSentTime)
                                    .ToListAsync();
            var unreadMessages = messages.Where(m=>m.DateRead==null && 
                                         m.Recipient.UserName==currentUserName).ToList();
           if(unreadMessages.Any()){
               foreach(var msg in unreadMessages) {
                   msg.DateRead= DateTime.Now;
               }
               await _context.SaveChangesAsync();
           }
           return _mapper.Map<IEnumerable<MessageDTO>>(messages);
        }

      
        public async Task<bool> saveAllAsync()
        {

            var allSaved = await _context.SaveChangesAsync();

            if (allSaved > 0)
            {
                return true;
            }
            return false;
        }
    }
}