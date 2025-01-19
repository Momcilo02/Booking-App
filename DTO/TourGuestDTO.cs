using BookingApp.Domain.Models;
using System.ComponentModel;

namespace BookingApp.DTO
{
    public class TourGuestDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }
        private string _name;
        public string Name { get => _name; set { if (value != _name) { _name = value; OnPropertyChanged(nameof(Name)); } } }

        public string FullName => $"{Name} {_surname}";
        private string _surname;
        public string Surname { get => _surname; set { if (value != _surname) { _surname = value; OnPropertyChanged(nameof(Surname)); } } }

        private int _age;
        public int Age { get => _age; set { if (value != _age) { _age = value; OnPropertyChanged(nameof(Age)); } } }

        private int _tourReservationId;
        public int TourReservationId { get => _tourReservationId; set { if (value != _tourReservationId) { _tourReservationId = value; OnPropertyChanged(nameof(TourReservationId)); } } }

        private int _checkPointId;
        public int CheckPointId { get => _checkPointId; set { if (value != _checkPointId) { _checkPointId = value; OnPropertyChanged(nameof(CheckPointId)); } } }

        private int _tourTimeId;
        public int TourTimeId { get => _tourTimeId; set { if (value != _tourTimeId) { _tourTimeId = value; OnPropertyChanged(nameof(TourTimeId)); } } }

        private int _userId;
        public int UserId { get => _userId; set { if (value != _userId) { _userId = value; OnPropertyChanged(nameof(UserId)); } } }

        private bool _confirmation;
        public bool Confirmation { get => _confirmation; set { if (value != _confirmation) { _confirmation = value; OnPropertyChanged(nameof(Confirmation)); } } }

        private bool _isSelected;
        public bool IsSelected { get => _isSelected; set { if (_isSelected != value) { _isSelected = value; OnPropertyChanged(nameof(IsSelected)); } } }

        private bool _presence;
        public bool Presence { get => _presence; set { if (value != _presence) { _presence = value; OnPropertyChanged(nameof(Presence)); } } }

        public TourGuestDTO() { }

        public TourGuestDTO(TourGuest tourGuest)
        {
            Id = tourGuest.Id; Name = tourGuest.Name; 
            Surname = tourGuest.Surname; 
            Age = tourGuest.Age;
            TourReservationId = tourGuest.TourReservationId; 
            CheckPointId = tourGuest.CheckPointId;
            TourTimeId = tourGuest.TourTimeId; 
            UserId = tourGuest.UserId; 
            Confirmation = tourGuest.Confirmation;
            Presence = tourGuest.Presence;
        }

        public TourGuest ToTourGuest() { return new(Id, _name, _surname, _age, _tourReservationId, _checkPointId, _tourTimeId, _userId, _confirmation, _presence); }

        protected virtual void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
    }
}
