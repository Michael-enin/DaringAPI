using System;
using System.Threading.Tasks;
using AutoMapper;
using DaringAPI.DTOs;
using DaringAPI.Entities;
using DaringAPI.Extensions;
using DaringAPI.interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DaringAPI.SignalR
{
  public class MessagesHub : Hub
  {

    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public MessagesHub(IMessageRepository messageRepository, IMapper mapper, IUserRepository userRepository)
    {
      this._userRepository = userRepository;
      this._messageRepository = messageRepository;
      this._mapper = mapper;

    }
    public override async Task OnConnectedAsync()
    {
      var httpContext = Context.GetHttpContext();
      var receiver = httpContext.Request.Query["user"].ToString();
      var groupName = GetGroupName(Context.User.GetUserName(), receiver);
      await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

      var messages = await _messageRepository
                     .GetMessageThread(Context.User.GetUserName(), receiver);
      await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
      await base.OnDisconnectedAsync(exception);
    }
    public async Task SendMessage(CreateMessageDTO createMessageDTO)
    {
      var userName = Context.User.GetUserName();

      if (userName == createMessageDTO.RecipientUserName)
      {
        throw new HubException("You can't Send Message to Yourself");
      }

      var sender = await _userRepository.GetUserByUserNameAsync(userName);
      var recipient = await _userRepository.GetUserByUserNameAsync(createMessageDTO.RecipientUserName);
      if (recipient == null)
    throw new HubException("User Not Found!");
      var message = new Message
      {
        Sender = sender,
        Recipient = recipient,
        SenderUserName = sender.KnownAs,
        RecipientUserName = recipient.KnownAs,
        Content = createMessageDTO.Content
      };
      _messageRepository.AddMessage(message);
      if (await _messageRepository.saveAllAsync()){
          var group = GetGroupName(sender.UserName, recipient.UserName);
          await Clients.Group(group).SendAsync("NewMessage", _mapper.Map<MessageDTO>(message));
          
      }
    }
    public string GetGroupName(string sender, string receiver)
    {
      var stringCompare = string.CompareOrdinal(sender, receiver) < 0;
      return stringCompare ? $"{sender}-{receiver}" : $"{receiver}-{sender}";
    }
  }
}