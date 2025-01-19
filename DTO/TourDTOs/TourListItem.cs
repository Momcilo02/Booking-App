using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO.TourDTOs
{
    public class TourListItem
    {
        public int Id { get; set; }
        public int TourTimeId { get; set; }

        public string ImagePath { get; set; }
        public string TourName { get; set; }
        public string Location { get; set; }
        public DateTime TourDate { get; set; }
    }
}
