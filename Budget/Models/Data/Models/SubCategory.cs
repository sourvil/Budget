using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Budget.Models.Data.Models
{
    public class SubCategory
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SubCategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Status { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

    }
}