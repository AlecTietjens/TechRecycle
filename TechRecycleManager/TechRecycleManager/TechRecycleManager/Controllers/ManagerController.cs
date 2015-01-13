using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TechRecycleManager.DAL;
using TechRecycleManager.Models;
using System.Data.Entity;

namespace TechRecycleManager.Controllers
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

            foreach (var t in db.Tickets.ToList())
            {
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
                ticket.IsHBIRequest = t.IsHBIRequest;
                ticket.AdditionalNotes = t.AdditionalNotes;

                if (ticket.PickupSize == "Bulk")
                {
                    var bulkDetails = db.BulkTicketDetails.Where(x => x.TicketNumber == ticket.TicketNumber).SingleOrDefault();
                    ticket.BinQuantity = bulkDetails.BinQuantity;
                    ticket.BinLocation1 = bulkDetails.BinLocation1;
                    ticket.BinLocation2 = bulkDetails.BinLocation2;
                    ticket.BinLocation3 = bulkDetails.BinLocation3;
                    ticket.BinLocation4 = bulkDetails.BinLocation4;
                    ticket.BinLocation5 = bulkDetails.BinLocation5;
                }

                if (ticket.IsHBIRequest == true)
                {
                    var hbiDetails = db.HBITicketDetails.Where(x => x.TicketNumber == ticket.TicketNumber).SingleOrDefault();
                    ticket.NeedsSecureBins = hbiDetails.NeedsSecureBins;
                    ticket.SecureBinQuantity = hbiDetails.SecureBinQuantity;
                    ticket.DestructionLocation = hbiDetails.DestructionLocation;
                    ticket.WitnessType = hbiDetails.WitnessType;
                }

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
