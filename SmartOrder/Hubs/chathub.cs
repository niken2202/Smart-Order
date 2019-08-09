using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartOrder.Hubs
{
    public class chathub:Hub
    {
        public async  Task sendMessage(string mess)
        {
            await Clients.Others.SendAsync(mess);
        }
    }
}