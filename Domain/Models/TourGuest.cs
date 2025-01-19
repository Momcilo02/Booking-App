using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class TourGuest : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int TourReservationId { get; set; }
        public int CheckPointId { get; set; }
        public int TourTimeId { get; set; }
        public int UserId { get; set; }
        public bool Confirmation { get; set; }
        public bool Presence { get; set; }
        public TourGuest()
        {

        }

        public TourGuest(string name, string surname, int age, int tourReservationId, int checkPointId, int tourTimeId, int userId, bool confirmation, bool presence)
        {
            Name = name;
            Surname = surname;
            Age = age;
            TourReservationId = tourReservationId;
            CheckPointId = checkPointId;
            TourTimeId = tourTimeId;
            UserId = userId;
            Confirmation = confirmation;
            Presence = presence;
        }
        public TourGuest(int id, string name, string surname, int age, int tourReservationId, int checkPointId, int tourTimeId, int userId, bool confirmation, bool presence)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            TourReservationId = tourReservationId;
            CheckPointId = checkPointId;
            TourTimeId = tourTimeId;
            UserId = userId;
            Confirmation = confirmation;
            Presence = presence;
        }
        /*
                                1|             Pera|   Peric|  22|              1|                          1|                  1|                      4|                  True|                   True
                                2|              Sima|  Simic|  33|              1|                          1|                  1|                      4|                  True|                   True
                                3|              Josn|  Snsow|  28|              2|                          1|                  2|                      4|                  True|                   True
                                4|              Nsed|  Stark|  38|              2|                          1|                  2|                      4|                  True|                   True
                                5|              David|  Peric|  17|              3|                          2|                  0|                      4|                  True|                   True
                                6|              Milan|  Milic|  17|              3|                          2|                  0|                      4|                  True|                   True
                                7|              Test|  Stark|  38|              3|                          2|                  0|                      4|                  True|                   True
                                8|              Test|  Stark|  38|              3|                          2|                  0|                      4|                  True|                   True
                                9|              Stark|  Stark|  38|              3|                          2|                  0|                      4|                  True|                   True
                                10|              Josn|  Stark|  59|              3|                          2|                  0|                      4|                  True|                   True
                                11|              Nsed|  Simic|  38|              3|                          2|                  0|                      4|                  True|                   True
        */
        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Name, Surname, Age.ToString(), TourReservationId.ToString(), TourTimeId.ToString(), CheckPointId.ToString(), UserId.ToString(), Confirmation.ToString(), Presence.ToString() };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = Convert.ToString(values[1]);
            Surname = Convert.ToString(values[2]);
            Age = Convert.ToInt32(values[3]);
            TourReservationId = Convert.ToInt32(values[4]);
            TourTimeId = Convert.ToInt32(values[5]);
            CheckPointId = Convert.ToInt32(values[6]);
            UserId = Convert.ToInt32(values[7]);
            Confirmation = Convert.ToBoolean(values[8]);
            Presence = Convert.ToBoolean(values[9]);
        }
    }
}
