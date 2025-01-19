using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuestReviewRepository
    {
        List<GuestReview> GetAll();
        int NextId();
        GuestReview Save(GuestReview guestReview);
        void Delete(GuestReview guestReview);
        GuestReview Update(GuestReview guestReview);
    }
}
