using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budget.Models.Data.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Note { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int Status { get; set; }

        public virtual Category Category { get; set; }
    }
}