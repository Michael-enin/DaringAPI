using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaringAPI.ClientUI.src.app.SignalR
{
    public class AvailabilityTracker
    {
        private static readonly Dictionary<string, List<string>> onlineUsers = new Dictionary<string, List<string>>();
        public Task UserConnected(string username, string connectionId)
        {
            lock(onlineUsers){
                if(onlineUsers.ContainsKey(username)){
                    onlineUsers[username].Add(connectionId);
                }
                else{
                    onlineUsers.Add(username, new List<string>{connectionId});
                }
            }
            return Task.CompletedTask;
        }
        public Task DisconnectedUser(string username, string connectionId){
                lock(onlineUsers){
                    if(!onlineUsers.ContainsKey(username))
                      return Task.CompletedTask;
                    onlineUsers[username].Remove(connectionId);
                    if(onlineUsers[username].Count == 0){
                        onlineUsers.Remove(username);
                    }
                }
                return Task.CompletedTask;
        }
        public Task<string[]> getOnlineUsers(){
            string[] on_lineUsers;
           lock(onlineUsers)
           {
               on_lineUsers = onlineUsers
                               .OrderBy(x =>x.Key)
                               .Select(x =>x.Key)
                               .ToArray();
           }
           return Task.FromResult(on_lineUsers);
        }
                          
    }
}