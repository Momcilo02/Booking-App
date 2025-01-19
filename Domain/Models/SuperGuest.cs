using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class SuperGuest : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int PointsRemaining { get; set; }
        public DateOnly EndDate { get; set; }
        public SuperGuest()
        {
            
        }
        public SuperGuest(int guestId, DateOnly end)
        {
            GuestId = guestId;
            PointsRemaining = 10;
            EndDate = end;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            PointsRemaining = Convert.ToInt32(values[2]);
            EndDate = DateOnly.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] values = {Id.ToString(), GuestId.ToString(), PointsRemaining.ToString(), EndDate.ToString()};
            return values;
        }
    }
}
