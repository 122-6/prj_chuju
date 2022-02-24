using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prj_chuju.SignalR
{
    public class chatHub : Hub
    {
        private static string serviceID;
        private static string clientID;
        
        public void ServiceConnection()
        {
            serviceID = Context.ConnectionId;
        }

        public void ClientConnection()
        {
            clientID = Context.ConnectionId;
            Clients.Client(clientID).newClient(clientID);
            if (serviceID != null)
            {
                Clients.Client(serviceID).newClient(clientID);
            }
        }

        public void ServiceSend(string message, string Id)
        {
            Clients.Client(Id).sendMessage(message);
        }

        public void ClientSend(string message, string Id)
        {
            if (serviceID != null)
            {
                Clients.Client(serviceID).sendMessage(message, Id);
            }
        }
    }
}