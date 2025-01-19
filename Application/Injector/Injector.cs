using BookingApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Application.Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
    {

            {typeof(IAccommodationRepository), new AccommodationRepository()},
            {typeof(IAccommodationReservationRepository), new AccommodationReservationRepository()},
            {typeof(IAccommodationReservationRequestRepository), new AccommodationReservationRequestRepository()},
            {typeof(ICheckPointRepository),new CheckPointRepository()},
            {typeof(ICommentRepository), new CommentRepository()},
            {typeof(IGuestReviewRepository), new GuestReviewRepository()},
            {typeof(IImageRepository), new ImageRepository()},
            {typeof(ILanguageRepository), new LanguageRepository() },
            {typeof(ILiveTourRepository), new LiveTourRepository() },
            {typeof(ILocationRepository), new LocationRepository()},
            {typeof(ITourGuestRepository), new TourGuestRepository()},
            {typeof(ITourRequestRepository), new TourRequestRepository()},
            {typeof(ITouristReviewRepository), new TouristReviewRepository()},
            {typeof(ITourRepository), new TourRepository()},
            {typeof(ITourReservationRepository), new TourReservationRepository()},
            {typeof(ITourTimeRepository), new TourTimeRepository()},
            {typeof(IUserRepository), new UserRepository()},
            {typeof(IVoucherRepository), new VoucherRepository()},

            { typeof(ISuperGuestRepository), new SuperGuestRepository()},
            { typeof(IAccommodationRenovationRepository),new AccommodationRenovationRepository()},
            {typeof(IAccommodationRenovationSuggestionRepository), new AccommodationRenovationSuggestionRepository()},
            {typeof(IAccommodationReviewRepository), new AccommodationReviewRepository()},
            { typeof(IAccommodationStatsRepository),new AccommodationStatRepository()},
            {typeof(IForumReposiroty), new ForumRepository()},
            {typeof(IForumCommentRepository), new ForumCommentRepository()}

    };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
