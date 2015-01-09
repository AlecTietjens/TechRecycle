using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TechRecycleClient.DAL;
using TechRecycleClient.Models;

namespace TechRecycleClient.Controllers
{
    public class ManagerController : Controller
    {
        private TicketsContext db = new TicketsContext();

        public ActionResult Index()
        {
            return View("Index");
        }

        public JsonResult GetAll()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            var tickets = new List<TicketViewModel>();

            foreach(var t in db.Tickets.ToList()){
                var ticket = new TicketViewModel();

                ticket.TicketNumber = t.TicketNumber;
                ticket.CurrentStatus = t.CurrentStatus;
                ticket.Alias = t.Alias;
                ticket.Name = t.Name;
                ticket.Phone = t.Phone;
                ticket.Email = t.Email;
                ticket.AlternateName = t.AlternateName;
                ticket.AlternatePhone = t.AlternatePhone;
                ticket.AlternateEmail = t.AlternateEmail;
                ticket.Building = t.Building;
                ticket.Location = t.Location;
                ticket.PickupSize = t.PickupSize;
                ticket.ComputerQuantity = t.ComputerQuantity;
                ticket.MonitorQuantity = t.MonitorQuantity;
                ticket.MiscQuantity = t.MiscQuantity;
                ticket.OpenDate = t.OpenDate.ToString();
                ticket.ModifyDate = t.ModifyDate.ToString();
                ticket.LastModifiedBy = t.LastModifiedBy;

                tickets.Add(ticket);
            }

            string output = jss.Serialize(tickets);

            return Json(output, JsonRequestBehavior.AllowGet);

            //var tickets = db.Tickets.ToList();

            //var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            //return JsonConvert.SerializeObject(tickets, Formatting.None, settings);
        }
    }
}
