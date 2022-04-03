using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace prj_chuju.SignalR
{
    public class chatHub : Hub
    {
        private static string serviceID;
        private static string clientID;
        dbchujuEntities1 db = new dbchujuEntities1();
        public void ServiceConnection()
        {
            //serviceID = Context.ConnectionId;

            ServiceConnection tservice = new ServiceConnection()
            {
                connectionId = Context.ConnectionId
            };
            db.ServiceConnection.Add(tservice);
            db.SaveChanges();
        }

        public void ClientConnection(string userName)
        {
            if (userName == null)
            {
                Random rnd = new Random();
                userName = "訪客" + rnd.Next(1000, 9999);
            }

            //client端連線資料傳入db
            ClientConnection tclient = new ClientConnection()
            {
                connectionId = Context.ConnectionId
            };
            db.ClientConnection.Add(tclient);

            clientID = Context.ConnectionId;
            
            if (db.ServiceConnection.Count() != 0)
            {
                //client端尋找連線人數最少的service端連線
                ServiceConnection Min = db.ServiceConnection.Where(t => t.online_count == db.ServiceConnection.Min(m => m.online_count)).FirstOrDefault();

                serviceID = Min.connectionId;

                Clients.Client(serviceID).newClient(clientID, userName);
                Clients.Client(clientID).newClient(clientID, serviceID);
                //service端有client加入連線後，連線人數+1
                ServiceConnection tservice = db.ServiceConnection.FirstOrDefault(p => p.connectionId == serviceID);
                if (tservice != null)
                {
                    tservice.online_count++;
                }
            }

            db.SaveChanges();
        }

        public void ServiceSend(string message, string Id)
        {
            if (Id != "" && message != "")
            {
                Clients.Client(Id).sendMessage(message);
            }
        }

        public void ClientSend(string message, string cId, string sId)
        {
            if (sId != null && message != "")
            {
                Clients.Client(sId).sendMessage(message, cId);
            }
            else
            {
                
            }
        }

        //public override Task OnDisconnected(bool stopCalled)
        //{
            
        //    return deleteId();
        //}

        //public Task deleteId()
        //{
        //    ClientConnection tclient = db.ClientConnection.FirstOrDefault(p => p.connectionId == Context.ConnectionId);
        //    ServiceConnection tservice = db.ServiceConnection.FirstOrDefault(p => p.connectionId == Context.ConnectionId);
        //    if (tclient != null)
        //    {
        //        db.ClientConnection.Remove(tclient);
        //    }
        //    else if (tservice != null)
        //    {
        //        db.ServiceConnection.Remove(tservice);
        //    }
        //    db.SaveChanges();
        //    return null;
        //}
    }
}