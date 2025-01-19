using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILiveTourRepository
    {
        List<LiveTour> GetAll();
        LiveTour Get(int id);
        int NextId();
        LiveTour Save(LiveTour liveTour);
        void Delete(LiveTour liveTour);
        LiveTour Update(LiveTour liveTour);
    }
}
