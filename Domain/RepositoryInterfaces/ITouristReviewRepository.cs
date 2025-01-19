using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITouristReviewRepository
    {
        List<TouristReview> GetAll();
        TouristReview Get(int id);
        TouristReview Save(TouristReview touristReview);
        int NextId();
        void Delete(TouristReview touristReview);
        TouristReview Update(TouristReview touristReview);
    }
}
