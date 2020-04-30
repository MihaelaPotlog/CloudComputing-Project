using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TemaAzure2.Chat;

namespace TemaAzure2
{
    public class ChatHub:Hub
    {
        public void BroadcastMessage(string name, string message)
        {
            bool badWords = ContentModerate.ExistsBadWords(message);
            Clients.All.SendAsync("broadcastMessage", name, message, badWords);
        }

        public void Echo(string name, string message)
        {
            Clients.Client(Context.ConnectionId).SendAsync("echo", name, message + " (echo from server)");
        }
    }
}
