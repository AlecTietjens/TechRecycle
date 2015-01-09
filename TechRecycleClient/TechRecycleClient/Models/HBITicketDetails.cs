using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechRecycleClient.Models
{
    public class HBITicketDetails
    {
        public int ID { get; set; }
        [Required]
        public string TicketNumber { get; set; }
        public Boolean NeedsSecureBins { get; set; }
        public int SecureBinQuantity { get; set; }
        public string DestructionLocation { get; set; }
        public string WitnessType { get; set; }
    }
}