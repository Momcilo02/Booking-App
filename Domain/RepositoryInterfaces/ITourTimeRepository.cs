using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourTimeRepository
    {
        List<TourTime> GetAll();
        TourTime Get(int id);
        int NextId();
        TourTime Save(TourTime tourTime);
        void Delete(TourTime tourTime);
        void DeleteById(int id);
        TourTime Update(TourTime tourTime);
        List<TourTime> GetByTourId(int tourId);
    }
}
