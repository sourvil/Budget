using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Budget.Models.Data.Models
{
    public class Category
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [Required,StringLength(100)]
        public string Name { get; set; }
        [Required]
        public bool CategoryType { get; set; }
        [Required]
        public int Status { get; set; }

        public virtual List<SubCategory> SubCategory { get; set; }
        [NotMapped]
        public int SubCategoryCount { get; set; }
    }
}