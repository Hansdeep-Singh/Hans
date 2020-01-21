using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class ImageCoordinates
    {
        [MaxLength(128)]
        public string ClientId { get; set; }
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double y1 { get; set; }
        public double y2 { get; set; }
        public double w { get; set; }
        public double h { get; set; }
    }
}