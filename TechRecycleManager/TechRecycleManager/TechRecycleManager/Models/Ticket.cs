using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechRecycleManager.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        [Required]
        public string TicketNumber { get; set; }
        [Required]
        public string Alias { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string AlternateName { get; set; }
        [Required]
        public string AlternatePhone { get; set; }
        [Required]
        public string AlternateEmail { get; set; }
        [Required]
        public string Building { get; set; }
        [Required]
        public string Location { get; set; }
        public int ComputerQuantity { get; set; }
        public int MonitorQuantity { get; set; }
        public int MiscQuantity { get; set; }
        [Required]
        public string PickupSize { get; set; }
        public Boolean IsHBIRequest { get; set; }
        public string AdditionalNotes { get; set; }
        public string CurrentStatus { get; set; }
        public string LastModifiedBy { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}