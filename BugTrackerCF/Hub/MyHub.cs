using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace BugTrackerCF.Hub
{
    public class MyHub : Microsoft.AspNet.SignalR.Hub
    {
        public void Hello()
        {
            //Clients.All.hello();
        }

        public void SendNote(string user, string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            //context.Clients.All.sendMessage(message);
            context.Clients.User(user).sendMessage(message);
        }

    }
}