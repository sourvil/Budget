﻿using System;
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

        public byte[] AmountEncrypted { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Note { get; set; }
        [Required]
        public int SubCategoryID { get; set; }
        [Required]
        public int Status { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}