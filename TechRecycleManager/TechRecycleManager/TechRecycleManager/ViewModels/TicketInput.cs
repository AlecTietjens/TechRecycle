using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechRecycleClient.Models
{
    public class TicketInput
    {
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
        [Required]
        public Boolean IsHBIRequest { get; set; }
        public Boolean NeedsSecureBins { get; set; }
        public int SecureBinQuantity { get; set; }
        public string DestructionLocation { get; set; }
        public string WitnessType { get; set; }
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
    }
}