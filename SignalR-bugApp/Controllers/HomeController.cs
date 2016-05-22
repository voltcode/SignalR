using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalR_bugApp.Models;

namespace SignalR_bugApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var task = Task.Run(() => SendMessage());            
            return View();
        }

        public async void SendMessage()
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            await Task.Delay(TimeSpan.FromSeconds(5));

            var msg = new Message(Guid.NewGuid(), "lala", new Wrapper(Guid.NewGuid()));

            hub.Clients.User("1").addMessage(msg);
            hub.Clients.All.addMessage(msg);
            hub.Clients.All.AddMessage("KURAAWAWA");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}