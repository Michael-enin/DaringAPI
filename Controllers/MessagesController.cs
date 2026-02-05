using System.Threading.Tasks;
using DaringAPI.DTOs;
using DaringAPI.Extensions;
using DaringAPI.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DaringAPI.Entities;
using AutoMapper;
using System.Collections.Generic;
using DaringAPI.Helpers;

namespace DaringAPI.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public MessagesController(IMessageRepository messageRepository,        
                                  IUserRepository userRepository, 
                                  IMapper mapper)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._messageRepository = messageRepository;
        }
    [HttpPost]
    public async Task<ActionResult<MessageDTO>> CreateMessage(CreateMessageDTO createMessageDTO)
    {
        var userName = User.GetUserName();       
        if (userName == createMessageDTO.RecipientUserName)
        { 
            return BadRequest("You can't Send Message to Yourself");
        }           
        var sender = await _userRepository.GetUserByUserNameAsync(userName);
        var recipient = await _userRepository.GetUserByUserNameAsync(createMessageDTO.RecipientUserName);
        if (recipient == null)
            return NotFound();
        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SenderUserName = sender.KnownAs,
            RecipientUserName = recipient.KnownAs,
            Content = createMessageDTO.Content
        };
        _messageRepository.AddMessage(message);
        if(await _messageRepository.saveAllAsync()) 
        return Ok(_mapper.Map<MessageDTO>(message));
        return BadRequest("Your Message Has Not Been Sent!");
      }
      [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessagesForUser([FromQuery] MessageParams messageParams){
       messageParams.UserName = User.GetUserName();
       var messages = await _messageRepository.GetMessagesForUser(messageParams);
       Response.AddPaginationHeader(messages.CurrentPage, 
                                    messages.PageSize, 
                                    messages.TotalCount, 
                                    messages.PageSize);
       return messages;
    }
// get Dictionary
     

    [HttpGet("thread/{username}")]
    public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessageThread(string username){
        var currentUserName = User.GetUserName();
        return Ok(await _messageRepository.GetMessageThread(currentUserName, username));

    } 
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(int id){
       var userName = User.GetUserName();
       var message = await _messageRepository.GetMessage(id);
       if(message.Sender.KnownAs != userName && message.Recipient.UserName != userName)
       return Unauthorized();
       if(message.Sender.KnownAs == userName) message.SenderDeleted = true;
       if(message.Recipient.KnownAs == userName) message.RecipientDeleted = true;
       if(message.SenderDeleted && message.RecipientDeleted){
           _messageRepository.DeleteMessage(message);
       }
       if( await _messageRepository.saveAllAsync())
       return Ok();
       return BadRequest("There is an internal error while deleting the message");
    }
    }
    
}