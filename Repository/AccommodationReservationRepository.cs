using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation-reservations.csv";
        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _accommodationReservations;
        private AccommodationRepository accommodationRepository;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationReservations = _serializer.FromCSV(FilePath);
            accommodationRepository = new AccommodationRepository();
            Initiate();
        }
        private void Initiate()
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            foreach(AccommodationReservation reservation in _accommodationReservations) {
                reservation.Accommodation = accommodationRepository.GetAccommodation(reservation.Accommodation.Id);
            }

        }

        public List<AccommodationReservation> GetAll()
        {
            _accommodationReservations.Clear();
            Initiate();
            return _accommodationReservations;
        }

        private int NextId()
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            if (_accommodationReservations.Count < 1)
            {
                return 1;
            }
            return _accommodationReservations.Max(x => x.Id) + 1;
        }
        public AccommodationReservation Get(int id)
        {
            List<AccommodationReservation> reservations = GetAll();
            return reservations.Find(l => l.Id == id);
        }
        public AccommodationReservation Save(AccommodationReservation accommodetionReservation)
        {
            accommodetionReservation.Id = NextId();
            _accommodationReservations = _serializer.FromCSV(FilePath);
            _accommodationReservations.Add(accommodetionReservation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return accommodetionReservation;
        }
        public void Delete(AccommodationReservation accommodationReservation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation founded = _accommodationReservations.Find(ar => ar.Id == accommodationReservation.Id);
            if (founded != null)
            {
                _accommodationReservations.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _accommodationReservations);
        }
        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation current = _accommodationReservations.Find(ar => ar.Id == accommodationReservation.Id);
            int index = _accommodationReservations.IndexOf(current);
            _accommodationReservations[index] = accommodationReservation;
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return accommodationReservation;
        }

        public List<AccommodationReservation> GetByAccommodation(Accommodation accommodation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.FindAll(ar => ar.Accommodation.Id == accommodation.Id);
        }

        public List<AccommodationReservation> GetByUser(User user)
        {
            _accommodationReservations.Clear();
            Initiate();
            return _accommodationReservations.FindAll(ar => ar.User.Id == user.Id);
        }
        public AccommodationReservation GetAccommodationReservationById(int id)
        {
            AccommodationReservation reservation = GetAll().FirstOrDefault(r => r.Id == id);

            return reservation;
        }
        public List<AccommodationReservation> GetReservationsWithinNDays(int period, List<AccommodationReservation> accommodationReservations)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
            DateOnly nDaysAgo = currentDate.AddDays(-period);
            var reservationsWithinNDays = accommodationReservations.Where(r => r.EndDate > nDaysAgo && r.EndDate <= currentDate).ToList();
            return reservationsWithinNDays;
        }

        public AccommodationReservation GetById(int id)
        {
            return _accommodationReservations.FirstOrDefault(r => r.Id == id);
        }
        public int GetReservationCountForAccommodationYear(int accommodationId, int year)
        {
             
            return _accommodationReservations.Count(r => r.Accommodation.Id == accommodationId && r.StartDate.Year == year);
        }
        public int GetReservationCountForAccommodationMonth(int accommodationId, int year,int month)
        {
            return _accommodationReservations.Count(r => r.Accommodation.Id == accommodationId && r.StartDate.Year == year && r.StartDate.Month==month);
        }
    }
}
