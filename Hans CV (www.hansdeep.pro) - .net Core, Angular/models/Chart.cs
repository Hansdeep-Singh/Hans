using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vc.models
{
    public class Chart
    {
        [Required]
        public int ChartId { get; set; }
        public string ChartName { get; set; }
        public int Valone { get; set; }
        public int Valtwo { get; set; }
        public int Valthree { get; set; }
        public int Valfour { get; set; }
        public int Valfive { get; set; }
        public int Valsix { get; set; }
        public int Valseven { get; set; }
    }
}
