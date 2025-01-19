using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationReservationRequestRepository:IAccommodationReservationRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/requests.csv";
        private readonly Serializer<AccommodationReservationRequest> _serializer;
        private AccommodationRepository _accommodationRepository;
        private List<AccommodationReservationRequest> _accommodationReservationRequests;
        private AccommodationReservationRepository _accommodationReservationRepository;

        public AccommodationReservationRequestRepository()
        {
            _serializer = new Serializer<AccommodationReservationRequest>();
            _accommodationReservationRequests = _serializer.FromCSV(FilePath);
            _accommodationRepository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
        }
        public List<AccommodationReservationRequest> GetAll()
        {
            _accommodationReservationRequests = _serializer.FromCSV(FilePath);
            foreach(AccommodationReservationRequest request in _accommodationReservationRequests)
            {
                request.OldReservation = _accommodationReservationRepository.Get(request.OldReservation.Id);
            }
            return _accommodationReservationRequests;
        }
        public int NextId()
        {
            _accommodationReservationRequests = _serializer.FromCSV(FilePath);
            if (_accommodationReservationRequests.Count < 1)
            {
                return 1;
            }
            return _accommodationReservationRequests.Max(x => x.Id) + 1;
        }
        public AccommodationReservationRequest Save(AccommodationReservationRequest accommodetionReservationRequest)
        {
            accommodetionReservationRequest.Id = NextId();
            _accommodationReservationRequests = _serializer.FromCSV(FilePath);
            _accommodationReservationRequests.Add(accommodetionReservationRequest);
            _serializer.ToCSV(FilePath, _accommodationReservationRequests);
            return accommodetionReservationRequest;
        }
        public AccommodationReservationRequest Delete(AccommodationReservationRequest accommodationReservationRequest)
        {
            _accommodationReservationRequests = _serializer.FromCSV(FilePath);
            AccommodationReservationRequest founded = _accommodationReservationRequests.Find(ar => ar.Id == accommodationReservationRequest.Id);
            if (founded != null)
            {
                _accommodationReservationRequests.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _accommodationReservationRequests);
            return accommodationReservationRequest;
        }
        public AccommodationReservationRequest Update(AccommodationReservationRequest accommodationReservationRequest)
        {
            _accommodationReservationRequests = _serializer.FromCSV(FilePath);
            AccommodationReservationRequest current = _accommodationReservationRequests.Find(ar => ar.Id == accommodationReservationRequest.Id);
            int index = _accommodationReservationRequests.IndexOf(current);
            _accommodationReservationRequests[index] = accommodationReservationRequest;
            _serializer.ToCSV(FilePath, _accommodationReservationRequests);
            return accommodationReservationRequest;
        }

        public AccommodationReservationRequest GetById(int id)
        {
            throw new NotImplementedException();
        }

        public AccommodationReservationRequest Delete(int id)
        {
            throw new NotImplementedException();
        }
        public int GetCancellationCountForAccommodationYear(int accommodationId, int year)
        {
            var ret =  _accommodationReservationRequests.Count(r => r.OldReservation.Id == accommodationId && r.NewStartDate.Year == year && r.Status== Enumeration.AccommodationReservationRquest.Approved);
            if (ret == null) {

                return 0;
            }
            return ret;
        }
        public int GetCancellationCountForAccommodationMonth(int accommodationId, int year,int month)
        {
            var ret =  _accommodationReservationRequests.Count(r => r.OldReservation.Id == accommodationId && r.NewStartDate.Year == year && r.Status == Enumeration.AccommodationReservationRquest.Approved && r.NewStartDate.Month==month);
            if (ret == null)
            {

                return 0;
            }
            return ret;
        }
    }
}
