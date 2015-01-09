using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechRecycleClient.Models
{
    public class BulkTicketDetails
    {
        public int ID { get; set; }
        [Required]
        public string TicketNumber { get; set; }
        public int BinQuantity { get; set; }
        public string BinLocation1 { get; set; }
        public string BinLocation2 { get; set; }
        public string BinLocation3 { get; set; }
        public string BinLocation4 { get; set; }
        public string BinLocation5 { get; set; }
    }
}