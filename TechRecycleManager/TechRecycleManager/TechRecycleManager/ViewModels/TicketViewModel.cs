using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechRecycleClient.Models
{
    public class TicketViewModel
    {
        public string TicketNumber { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AlternateName { get; set; }
        public string AlternatePhone { get; set; }
        public string AlternateEmail { get; set; }
        public string Building { get; set; }
        public string Location { get; set; }
        public int ComputerQuantity { get; set; }
        public int MonitorQuantity { get; set; }
        public int MiscQuantity { get; set; }
        public string PickupSize { get; set; }
        public int BinQuantity { get; set; }
        public string BinLocation1 { get; set; }
        public string BinLocation2 { get; set; }
        public string BinLocation3 { get; set; }
        public string BinLocation4 { get; set; }
        public string BinLocation5 { get; set; }
        public string AdditionalNotes { get; set; }
        public string CurrentStatus { get; set; }
        public string LastModifiedBy { get; set; }
        public string OpenDate { get; set; }
        public string ModifyDate { get; set; }
        public string CloseDate { get; set; }
    }
}