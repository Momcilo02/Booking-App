using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourGuestRepository
    {
        List<TourGuest> GetAll();
        int NextId();
        TourGuest Save(TourGuest tour);
        TourGuest UpdateConfirmation(int tourGuestId, bool confirmation);
        TourGuest Update(TourGuest tourGuest);
        void Delete(TourGuest tour);
    }
}
