using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudCord.Models
{
    public class XYListViewModel
    {
        public int izracunId { get; set; }
        public string tagIz { get; set; }
        public double xCoord { get; set; }
        public double yCoord { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00#}")]
        public double? ECoord { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00#}")]
        public double? NCoord { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime createdIz { get; set; }
    }
}