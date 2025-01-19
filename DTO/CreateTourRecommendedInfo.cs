using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class CreateTourRecommendedInfo
    {
        public Language? SelectedLanguage { get; set; }
        public LocationDTO? SelectedLocation { get; set; }
    }
}
