using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{

    public class ReservationRepository : Repository<Reservation>
    {
        public void AddReservation(Reservation r)
        {
            Add(r); Save();
        }

        public Reservation Reservation(string id)
        {
            return DbSet.Where(r => r.ReservationId.Equals(id)).SingleOrDefault();
        }

        public List<Reservation> GetAllReservations()
        {
            return DbSet.ToList();
        }

        public void EntryDetatch(Reservation r)
        {
            EntryDetached(r);
        }

        public void EntryAttach(Reservation r)
        {
            EntryAttach(r);
        }

        public List<Reservation> Reservations(string id)
        {
            return DbSet.Where(i => i.ReservationId.Equals(id)).ToList();
        }


        public void DeleteReservation(Reservation r)
        {
            Delete(r); Save();
        }


        public void UpdateReservation(Reservation r)
        {
            Entry(r); Save();
        }

        public Boolean IfExists(int id)
        {
            if (DbSet.Any(d => d.ReservationId.Equals(id)))
                return true;
            else
                return false;
        }
    }
}
