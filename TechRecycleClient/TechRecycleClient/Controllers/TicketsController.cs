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
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace TechRecycleClient.Controllers
{
    public class TicketsController : Controller
    {
        private TicketsContext db = new TicketsContext();

        public ActionResult Index()
        {
            //string dc = "ldap://redmond.corp.microsoft.com";

            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, dc);
            //UserPrincipal u = UserPrincipal.FindByIdentity(ctx, "v-altiet");

            //string firstname = u.GivenName;
            //string lastname = u.Surname;
            //string email = u.EmailAddress;
            //string telephone = u.VoiceTelephoneNumber;
            //string company = String.Empty;

            ////how I can get company and other properties?
            //if (userPrincipal.GetUnderlyingObjectType() == typeof(DirectoryEntry))
            //{
            //    // Transition to directory entry to get other properties
            //    using (var entry = (DirectoryEntry)userPrincipal.GetUnderlyingObject())
            //    {
            //        if (entry.Properties["company"] != null)
            //            company = entry.Properties["company"].Value.ToString();
            //    }
            //}
            string alias = "";
            try
            {
                alias = User.Identity.Name.Replace(@"REDMOND\", "");

                DirectorySearcher dirSearcher = new DirectorySearcher();
                DirectoryEntry entry = new DirectoryEntry(dirSearcher.SearchRoot.Path);
                dirSearcher.Filter = "(&(objectClass=user)(objectcategory=person)(mail=" + alias + "*))";

                SearchResult srEmail = dirSearcher.FindOne();

                ResultPropertyValueCollection emailColl = srEmail.Properties["mail"];
                ResultPropertyValueCollection fnColl = srEmail.Properties["givenname"];
                ResultPropertyValueCollection snColl = srEmail.Properties["sn"];
                ResultPropertyValueCollection phColl = srEmail.Properties["telephonenumber"];

                string email = emailColl[0].ToString();
                string name = fnColl[0].ToString() + " " + snColl[0].ToString();
                string phone = "";
                if (phColl.Count > 0)
                    phone = phColl[0].ToString();

                ViewBag.Alias = alias;
                ViewBag.Email = email;
                ViewBag.Name = name;
                ViewBag.Phone = phone;
            }
            catch (Exception e)
            {
                ViewBag.Alias = "";
                ViewBag.Email = "";
                ViewBag.Name = "";
                ViewBag.Phone = "";
            }
            return View("Index", model: GetByAlias(alias));
        }

        public ActionResult Create(TicketInput ticketInput)
        {
            Ticket ticket = new Ticket();
            HBITicketDetails hbiItems = new HBITicketDetails();
            BulkTicketDetails bulkItems = new BulkTicketDetails();

            ticket.TicketNumber = "SR-" + db.Tickets.Count().ToString().PadLeft(6, '0');

            ticket.Alias = ticketInput.Alias;
            ticket.Name = ticketInput.Name;
            ticket.Phone = ticketInput.Phone;
            ticket.Email = ticketInput.Email;
            ticket.AlternateName = ticketInput.AlternateName;
            ticket.AlternatePhone = ticketInput.AlternatePhone;
            ticket.AlternateEmail = ticketInput.AlternateEmail;
            ticket.Building = ticketInput.Building;
            ticket.Location = ticketInput.Location;
            ticket.ComputerQuantity = ticketInput.ComputerQuantity;
            ticket.MonitorQuantity = ticketInput.MonitorQuantity;
            ticket.MiscQuantity = ticketInput.MiscQuantity;
            ticket.PickupSize = ticketInput.PickupSize;
            ticket.IsHBIRequest = ticketInput.IsHBIRequest;

            if (ticket.IsHBIRequest == true)
            {
                hbiItems.TicketNumber = ticket.TicketNumber;
                hbiItems.NeedsSecureBins = ticketInput.NeedsSecureBins;
                hbiItems.SecureBinQuantity = ticketInput.SecureBinQuantity;
                hbiItems.DestructionLocation = ticketInput.DestructionLocation;
                hbiItems.WitnessType = ticketInput.WitnessType;
            }

            if (ticket.PickupSize == "Bulk")
            {
                bulkItems.TicketNumber = ticket.TicketNumber;
                bulkItems.BinQuantity = ticketInput.BinQuantity;
                bulkItems.BinLocation1 = ticketInput.BinLocation1;
                bulkItems.BinLocation2 = ticketInput.BinLocation2;
                bulkItems.BinLocation3 = ticketInput.BinLocation3;
                bulkItems.BinLocation4 = ticketInput.BinLocation4;
                bulkItems.BinLocation5 = ticketInput.BinLocation5;
            }

            ticket.AdditionalNotes = ticketInput.AdditionalNotes;
            ticket.OpenDate = DateTime.Now;


            ticket.CurrentStatus = "Open";
            try
            {
                db.Tickets.Add(ticket);
                if (ticket.IsHBIRequest == true)
                    db.HBITicketDetails.Add(hbiItems);
                if (ticket.PickupSize == "Bulk")
                    db.BulkTicketDetails.Add(bulkItems);

                db.SaveChanges();

                var result = new
                {
                    TicketNumber = ticket.TicketNumber,
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Index");

        }

        public string GetByAlias(string id)
        {
            var tickets = db.Tickets.Where(x => x.Alias == id);

            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            return JsonConvert.SerializeObject(tickets, Formatting.None, settings);

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //string output = jss.Serialize(tickets);

            //return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}
