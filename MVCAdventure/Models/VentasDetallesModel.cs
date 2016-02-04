using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAdventure.Models
{
    public class VentasDetallesModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

    }
}


