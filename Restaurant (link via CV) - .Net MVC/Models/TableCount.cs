using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class TableCount
    {
        public int TableCountId { get; set; }
        public string TableName { get; set; }
        public int CurrentCount { get; set; }
        public int OldCount { get; set; }
        public int NewCount { get; set; }
        public string DateAdded { get; set; }
    }
}