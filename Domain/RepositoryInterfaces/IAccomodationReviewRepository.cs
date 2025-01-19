using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReviewRepository
    {
        List<AccommodationReview> GetAll();
        int NextId();
        AccommodationReview Save(AccommodationReview review);
        AccommodationReview Delete(AccommodationReview review);
        AccommodationReview Update(AccommodationReview review);
        List<AccommodationReview> GetByUser(User user);
        AccommodationReview Get(int id);
    }
}
