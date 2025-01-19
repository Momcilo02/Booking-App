using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class LiveTourService
    {
        private readonly ILiveTourRepository _liveTourRepository;

        public LiveTourService()
        {
            _liveTourRepository = Injector.Injector.CreateInstance<ILiveTourRepository>();
        }
        public List<LiveTour> GetAll()
        {
            return _liveTourRepository.GetAll();
        }


        public LiveTour Get(int id)
        {
            return _liveTourRepository.Get(id);
        }
        public LiveTour Save(LiveTour liveTour)
        {
            return _liveTourRepository.Save(liveTour);
        }

        public LiveTour Update(LiveTour liveTour)
        {
            return _liveTourRepository.Update(liveTour);
        }

        public void Delete(LiveTour liveTour)
        {
            _liveTourRepository.Delete(liveTour);
        }
    }
}
