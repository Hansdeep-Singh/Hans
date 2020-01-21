using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class PageReservationViewModel
    {
        public ReservationViewModel Rvm { get; set; }
        public List<ReservationViewModel> Rvml { get; set; }
    }
}