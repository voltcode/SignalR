using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace SignalR_bugApp.Models
{
    public class MyHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }
}