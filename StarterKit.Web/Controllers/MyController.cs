using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MassTransit;

namespace StarterKit.Web.Controllers
{
    using System.Threading.Tasks;
    using Contracts;
    using ViewModels;


    public class MyController : Controller
    {
        private readonly IBus _bus;

        public MyController(IBus bus)
        {
            _bus = bus;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Submit(MyMessageViewModel model)
        {
            await _bus.Publish<MyMessage>(new
            {
                Message = model.Text ?? "Unknown"
            });

            return View("Index");
        }
    }
}