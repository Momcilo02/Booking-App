using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourRequestFilterOptions
    {
        public string SelectedCity { get; set; }
        public string SelectedCountry { get; set; }
        public int? MaxNumberOfTourists { get; set; }
        public Language? SelectedLanguage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsComplexRequest { get; set; }

        public TourRequestFilterOptions()
        {
            SelectedCity = string.Empty;
            SelectedCountry = string.Empty;
            IsComplexRequest = false;
        }
    }


}
