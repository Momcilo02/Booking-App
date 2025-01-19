using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class SuperGuestService
    {
        private readonly ISuperGuestRepository superGuestRepository;
        private readonly IAccommodationReservationRepository accommodationReservationRepository;
        public SuperGuestService()
        {
            superGuestRepository = Injector.Injector.CreateInstance<ISuperGuestRepository>();
            accommodationReservationRepository = Injector.Injector.CreateInstance<IAccommodationReservationRepository>();
        }
        public List<SuperGuest> GetAll()
        {
            return superGuestRepository.GetAll();
        }
        public bool IsSuperGuest(int guestId)
        {
            List<SuperGuest> superGuests = superGuestRepository.GetAll();
            foreach (SuperGuest guest in superGuests)
            {
                if (guest.GuestId == guestId)
                {
                    return true;
                }
            }
            return false;
        }
        
        public int GetRemainingPoints(int guestId)
        {
            if (IsSuperGuest(guestId))
            {
                List<SuperGuest> superGuests = superGuestRepository.GetAll();
                SuperGuest super = superGuests.Find(s => s.GuestId == guestId);
                return super.PointsRemaining;
            }
            return 0;
        }
        public int IsGuestEligible(User user)
        {

            if(!IsSuperGuest(user.Id) && GetReservationNuberInLastYear(user) >= 10)
            {
                UpgradeToSuperGuest(user);
                return 0;
            }
            else if(IsSuperGuest(user.Id) && superGuestRepository.GetByGuestId(user.Id).EndDate.ToDateTime(TimeOnly.Parse("10:00PM")) < DateTime.Now)
            {
                superGuestRepository.Delete(superGuestRepository.GetByGuestId(user.Id));
                return 1;
            }
            return -1;
        }
        private int GetReservationNuberInLastYear(User user)
        {
            int counter = 0;
            foreach(AccommodationReservation res in accommodationReservationRepository.GetByUser(user))
            {
                DateTime resEndDate = res.EndDate.ToDateTime(TimeOnly.Parse("10:00PM"));
                if(resEndDate >= DateTime.Now.AddYears(-1))
                {
                    counter++;
                }
            }
            return counter;
        }
        public void UpgradeToSuperGuest(User user)
        {
            DateOnly end = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
            superGuestRepository.Save(new SuperGuest (user.Id, end));
        }
    }
}
