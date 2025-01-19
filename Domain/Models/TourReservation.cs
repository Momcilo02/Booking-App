using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Domain.Models
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public int TourTimeId { get; set; }
        public int CheckPointId { get; set; }

        public int LiveTourId { get; set; }
        public int IsActive { get; set; }

        public TourReservation()
        {

        }

        public TourReservation(int userId, int tourId, int tourTimeId, int checkPointId, int isActive)
        {
            UserId = userId;
            TourId = tourId;
            TourTimeId = tourTimeId;
            CheckPointId = checkPointId;
            IsActive = -1;
        }
        public TourReservation(int id, int userId, int tourId, int tourTimeId, int checkPointId, int isActive, int liveTourId)
        {
            Id = id;
            UserId = userId;
            TourId = tourId;
            TourTimeId = tourTimeId;
            CheckPointId = checkPointId;
            IsActive = -1;
            LiveTourId = liveTourId;
        }

        // 1|          4|              1|                  1|                  0|                          -1|2
        // 2|          4|              1|                  1|                  0|                          -1|2
        // 3|          4|              1|                  1|                  0|                          -1|0

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), UserId.ToString(), TourId.ToString(), TourTimeId.ToString(), CheckPointId.ToString(), IsActive.ToString(), LiveTourId.ToString() };
            return values;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            TourTimeId = Convert.ToInt32(values[3]);
            CheckPointId = Convert.ToInt32(values[4]);
            IsActive = Convert.ToInt32(values[5]);
            LiveTourId = Convert.ToInt32(values[6]);
        }



    }
}
