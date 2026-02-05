using System;
using System.Threading.Tasks;
using DaringAPI.ClientUI.src.app.SignalR;
using DaringAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DaringAPI.SignalR
{
  [Authorize]
  public class AvailableUserHub : Hub
  {
    private readonly AvailabilityTracker _tracker;

    public AvailableUserHub(AvailabilityTracker tracker)
    {
      this._tracker = tracker;
    }

    public override async Task OnConnectedAsync()
    {
      await _tracker.UserConnected(Context.User.GetUserName(), Context.ConnectionId);
      await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUserName());
      var availableUsers = await _tracker.getOnlineUsers();
      await Clients.All.SendAsync("GetOnlineUsers", availableUsers);
      
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
      await _tracker.DisconnectedUser(Context.User.GetUserName(), Context.ConnectionId);
      await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUserName());
      

       var availableUsers = await _tracker.getOnlineUsers();
      await Clients.All.SendAsync("GetOnlineUsers", availableUsers);

      await base.OnDisconnectedAsync(exception);
    }

  }
}