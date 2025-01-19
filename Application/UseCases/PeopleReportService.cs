using BookingApp.Domain.Models;
using BookingApp.DTO;
using System.Collections.Generic;

namespace BookingApp.Application.UseCases
{
    public class PeopleReportService
    {
        private readonly TourGuestService _tourGuestService;

        public PeopleReportService()
        {
            _tourGuestService = new TourGuestService();
        }

        public void SaveGuestDataForTourReservation(List<string> guestNames, int currentGuestIndex, TourDTO selectedTour, TourTimeDTO selectedTime, User user, string firstName, string lastName, string age)
        {
            if (currentGuestIndex < guestNames.Count)
            {
                string fullName = $"{firstName} {lastName}";
                string ageString = $"Age: {age}";
                guestNames[currentGuestIndex] = $"{fullName}, {ageString}";

                var guest = new TourGuest
                {
                    Name = firstName.Trim(),
                    Surname = lastName.Trim(),
                    Age = int.Parse(age.Trim()),
                    TourReservationId = selectedTour.Id,
                    TourTimeId = selectedTime.Id,
                    CheckPointId = 0,
                    UserId = user.Id,
                    Confirmation = false,
                    Presence = false
                };

                _tourGuestService.Save(guest);
            }
        }
        public void SaveGuestDataForTourRequest(List<string> guestNames, int currentGuestIndex,string firstName, string lastName, string age)
        {
            if (currentGuestIndex < guestNames.Count)
            {
                string fullName = $"{firstName} {lastName}";
                string ageString = $"Age: {age}";
                guestNames[currentGuestIndex] = $"{fullName}, {ageString}";

                var guest = new TourGuest
                {
                    Name = firstName.Trim(),
                    Surname = lastName.Trim(),
                    Age = int.Parse(age.Trim()),
                    TourReservationId = 0,
                    TourTimeId = 0,
                    CheckPointId = 0,
                    UserId = 0,
                    Confirmation = false,
                    Presence = false
                };

                _tourGuestService.Save(guest);
            }
        }
    }
}
