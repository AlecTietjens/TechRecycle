using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechRecycleManager.Models
{
    public class ScheduleInfoInput
    {
        [Required]
        public string TicketNumber { get; set; }
        [Required]
        public string Datetime { get; set; }
        [Required]
        public string Alias { get; set; }
    }
}