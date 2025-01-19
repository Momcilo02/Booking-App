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
    public class TourTimeService
    {
        private ITourTimeRepository _tourTimeRepository;

        public TourTimeService()
        {
            _tourTimeRepository = Injector.Injector.CreateInstance<ITourTimeRepository>();
        }
        public List<TourTime> GetAll()
        {
            return _tourTimeRepository.GetAll();
        }

        public List<TourTime> GetTourTimesByTourId(int TourId)
        {
            return _tourTimeRepository.GetAll().Where(x => x.TourId == TourId).ToList();
        }
        public TourTime Save(TourTime voucher)
        {
            return _tourTimeRepository.Save(voucher);
        }

        public TourTime Get(int id)
        {
            return _tourTimeRepository.Get(id);
        }
        public void Delete(TourTime voucher)
        {
            _tourTimeRepository.Delete(voucher);
        }

        public void DeleteById(int id)
        {
            _tourTimeRepository.DeleteById(id);
        }
        public List<TourTime> GetTourTimesForTour(int tourId)
        {
            return GetAll().Where(t => t.TourId == tourId).ToList();
        }
    }
}
