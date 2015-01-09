using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using TechRecycleClient.Models;

namespace TechRecycleClient.DAL
{
    public class TicketsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TicketsContext>
    {
        protected override void Seed(TicketsContext context)
        {
            string[] firstNames = new string[] {"James","John","Robert","Michael","William","David","Richard","Charles","Joseph","Thomas","Christopher","Mary","Patricia","Linda","Barbara",
                "Elizabeth","Jennifer","Maria","Susan","Margaret","Dorothy","Lisa","Nancy","Karen"};

            string[] lastNames = new string[] {"Smith","Johnson","Williams","Jones","Brown","Davis","Miller","Wilson","Moore","Taylor","Anderson","Thomas","Jackson","White","Harris","Martin",
            "Thompson","Garcia","Martinez","Rboinson","Clark","Rodriguez"};

            string[] emailDomains = new string[] { "yahoo.com", "gmail.com", "hotmail.com", "live.com", "microsoft.com" };

            string[] buildings = new string[] {"1", "10", "109", "11", "110", "111", "112", "113", "114", "115", "120", "121",
        "122", "123", "124", "125", "126", "127", "16", "17", "18", "19", "2", "21", "22", "24", "25", "26", "27", "28",
        "3", "30", "31", "32", "33", "34", "35", "36", "37", "4", "40", "41", "42", "43", "44", "5", "50", "6", "8",
        "84", "85", "86", "87", "88", "9", "92", "99", "Redwest A", "Redwest B", "Redwest C", "Redwest D", "Redwest E",
        "Redwest F", "Redwoods A", "Redwoods B", "Redwoods C", "Studio A", "Studio B", "Studio C", "Studio D", "Studio E",
        "Studio F", "Studio G", "Studio H", "Commons", "Kirkland 434", "Kirkland 550", "Advanta A", "Advanta C",
        "Bravern 1","Bravern 2"};

            string[] locations = new string[] { "Items will be placed outside of door", "Look beside the desk in office", "Monitors will be in the hallway", "Somewhere in hallway", "Contact me when picking up" };

            string[] additionaldetails = new string[] {"Four loko mixtape Portland kale chips ad readymade. Wolf salvia whatever mollit, cray bitters Marfa. Sustainable DIY irony PBR&B Pitchfork. Drinking vinegar fanny pack Cosby sweater vegan Bushwick raw denim. ",
            "Pickled craft beer mlkshk flexitarian PBR&B Pinterest. Organic stumptown quinoa plaid scenester, hoodie direct trade polaroid Intelligentsia Portland gastropub Banksy cred Neutra Pinterest. Lo-fi seitan synth fap pug twee.",
            "Twee four dollar toast letterpress, drinking vinegar whatever retro before they sold out try-hard leggings Pinterest 3 wolf moon.",
            "N/A",
            "High Life aesthetic Kickstarter bitters Wes Anderson artisan. ",
            "Pickled craft beer mlkshk flexitarian PBR&B Pinterest. Organic stumptown quinoa plaid scenester, hoodie direct trade polaroid Intelligentsia Portland gastropub Banksy cred Neutra Pinterest.",
            "Shoreditch meggings wolf semiotics four dollar toast food truck. PBR&B farm-to-table cold-pressed brunch."};

            var r = new Random();

            var tickets = new List<Ticket>();
            var bulkLocations = new List<BulkTicketDetails>();

            var date = new DateTime(2014, 11, 19, 8, 0, 0);

            for (int i = 0; i < 400;i++)
            {
                var ticket = new Ticket();

                var firstName = firstNames[r.Next(firstNames.Length)];
                var lastName = lastNames[r.Next(lastNames.Length)];
                ticket.Name = firstName + " " + lastName;

                ticket.Alias = "v-" + firstName;

                var randomNum = r.Next(10);
                if (randomNum > 3)
                {

                    ticket.TicketNumber = "SR-" + i.ToString().PadLeft(6, '0');
                }
                else
                {
                    ticket.TicketNumber = "HBI-" + i.ToString().PadLeft(6, '0');
                }

                var phoneNumber = "";
                for (int j = 0; j < 10; j++)
                {
                    phoneNumber += r.Next(10).ToString();
                }
                phoneNumber.Insert(3, "-");
                phoneNumber.Insert(7, "-");
                ticket.Phone = phoneNumber;

                ticket.Email = firstName + lastName + "@" + emailDomains[r.Next(emailDomains.Length)];

                firstName = firstNames[r.Next(firstNames.Length)];
                lastName = lastNames[r.Next(lastNames.Length)];
                ticket.AlternateName = firstName + " " + lastName;

                phoneNumber = "";
                for (int j = 0; j < 10; j++)
                {
                    phoneNumber += r.Next(10).ToString();
                }
                phoneNumber.Insert(3, "-");
                phoneNumber.Insert(6, "-");
                ticket.AlternatePhone = phoneNumber;

                ticket.AlternateEmail = firstName + lastName + "@" + emailDomains[r.Next(emailDomains.Length)];

                ticket.Building = buildings[r.Next(buildings.Length)];

                ticket.Location = locations[r.Next(locations.Length)];

                ticket.ComputerQuantity = r.Next(20);

                ticket.MonitorQuantity = r.Next(20);

                ticket.MiscQuantity = r.Next(20);

                var PickupClassification = r.Next(2);
                switch (PickupClassification)
                {
                    case 1: ticket.PickupSize = "Bulk";
                        break;
                    case 0: ticket.PickupSize = "Individual";
                        break;
                    default:
                        break;
                }

                if (PickupClassification == 1)
                {
                    var bulkDetails = new BulkTicketDetails();
                    bulkDetails.TicketNumber = ticket.TicketNumber;
                    bulkDetails.BinQuantity = r.Next(5);
                    for (i = 0; i < bulkDetails.BinQuantity; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                bulkDetails.BinLocation1 = locations[r.Next(locations.Length)];
                                break;
                            case 1:
                                bulkDetails.BinLocation2 = locations[r.Next(locations.Length)];
                                break;
                            case 2:
                                bulkDetails.BinLocation3 = locations[r.Next(locations.Length)];
                                break;
                            case 3:
                                bulkDetails.BinLocation4 = locations[r.Next(locations.Length)];
                                break;
                            case 4:
                                bulkDetails.BinLocation5 = locations[r.Next(locations.Length)];
                                break;
                            default:
                                // Loop should not reach this case
                                break;
                        }
                    }
                }

                ticket.AdditionalNotes = additionaldetails[r.Next(additionaldetails.Length)];

                var tempDate = date;
                for (; ; )
                {
                    tempDate.AddTicks(r.Next(1, 100000) * 10000000);
                    if (tempDate.Hour > 8 && tempDate.Hour < 19 && tempDate.DayOfWeek.ToString() != "Saturday" && tempDate.DayOfWeek.ToString() != "Sunday")
                    {
                        date = tempDate;
                        break;
                    }
                }
                ticket.OpenDate = date;
                ticket.CurrentStatus = "Open";



                if (ticket.OpenDate.CompareTo(DateTime.Now.AddDays(-5)) < 1)
                {
                    tempDate = date;
                    ticket.ModifyDate = tempDate.AddTicks(r.Next(300, 500000) * 100000000);
                    ticket.CurrentStatus = "In Progress";
                }
                else
                {
                    ticket.ModifyDate = null;
                }


                if (tempDate.CompareTo(DateTime.Now.AddDays(-10)) < 1)
                {
                    ticket.CloseDate = tempDate.AddTicks(r.Next(3000, 700000) * 100000000);
                    ticket.CurrentStatus = "Closed";
                }
                else
                {
                    ticket.CloseDate = null;
                }

                ticket.CurrentStatus = "Open";

                tickets.Add(ticket);
            }





            //var tickets = new List<Ticket>
            //{
            //    new Ticket{TicketNumber="SR-1234",Name="Mario",Phone="123-456-7890",Email="someone@someplace.com",Building="123",Location="Downstairs",ComputerQuantity=12,MonitorQuantity=16,MiscQuantity=3,TypeOfPickup="HBI",BinQuantity=2,BinDetails="Place Outside of Door",AdditionalNotes="Nope",CurrentStatus="Close",StartDate=new DateTime(2000,1,1),ModifyDate=new DateTime(2001,2,3),CloseDate=new DateTime(2002,3,4)},
            //    new Ticket{TicketNumber="SR-1235",Name="Luigi",Phone="123-456-4567",Email="somewhere@else.net",Building="124",Location="Upstairs",ComputerQuantity=3,MonitorQuantity=4,MiscQuantity=3,TypeOfPickup="LBI",BinQuantity=0,BinDetails="Maybe Later",AdditionalNotes="Help me ASAP",CurrentStatus="In Progress",StartDate=new DateTime(2001,2,4),ModifyDate=new DateTime(2001,5,5),CloseDate=new DateTime(1990,7,7)}
            //};

            tickets.ForEach(t => context.Tickets.Add(t));
            context.SaveChanges();
        }
    }
}