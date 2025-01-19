using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class GuestReviewService
    {
        private readonly IGuestReviewRepository guestReviewRepository;
        private readonly IAccommodationReviewRepository accommodationReviewRepository;
        private readonly IUserRepository userRepository;
        private readonly IAccommodationReservationRepository accommodationReservationRepository;
        public GuestReviewService()
        {
            guestReviewRepository = Injector.Injector.CreateInstance<IGuestReviewRepository>();
            accommodationReviewRepository = Injector.Injector.CreateInstance<IAccommodationReviewRepository>();
            userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            accommodationReservationRepository = Injector.Injector.CreateInstance<IAccommodationReservationRepository>();
        }
        public List<GuestReview> GetAll()
        {
            return guestReviewRepository.GetAll();
        }
        public List<GuestReview> GetAllForGuest(User guest)
        {
            List<GuestReview> reviews = new List<GuestReview>();
            List<AccommodationReview> accommodationReviews = accommodationReviewRepository.GetByUser(guest);
            foreach (GuestReview review in guestReviewRepository.GetAll())
            {
                review.AccommodationReservation= accommodationReservationRepository.GetById(review.AccommodationReservation.Id);
                review.AccommodationReservation.User = userRepository.GetById(review.AccommodationReservation.User.Id);
                if(review.AccommodationReservation.User.Id == guest.Id)
                {
                    foreach(AccommodationReview ar in accommodationReviews)
                    {
                        if(ar.Accommodation.Id == review.AccommodationReservation.Accommodation.Id)
                        {
                            reviews.Add(review);
                            break;
                        }
                    }
                    
                }
            }

            return reviews;
        }

    }
}
