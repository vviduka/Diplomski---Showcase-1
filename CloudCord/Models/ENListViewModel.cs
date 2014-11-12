using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudCord.Models
{
    public class ENListViewModel
    {
        public int izracunId { get; set; }
        public string tagIz { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime createdIz { get; set; }
        
        public double Eistok { get; set; }
        public double Nsjever { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:#,##0.00#}")]
        public double? xp { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00#}")]
        public double? yp { get; set; }

    }
}