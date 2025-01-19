using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel
{
    public class ReservationFinalViewModel:BindableBase
    {
        public AccommodationRenovationDTO Renovation { get; set; }
        public ObservableCollection<DateTime> _dates { get; set; }
        private AccommodationRenovationService service;
        public ICommand NavigationClick { get; }
        public DateTime Start { get; set; }
        public string Comment { get; set; }
        public DateTime End { get; set; }

        public int Duration { get; set; }
        private DateTime _selectedRequest;
        public DateTime SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                if (_selectedRequest != value)
                {
                    _selectedRequest = value;
                    OnPropertyChanged(nameof(SelectedRequest));
                }
            }
        }
        public ReservationFinalViewModel(AccommodationRenovationDTO renovation, DateTime start, DateTime end, int duration)
        {
            Renovation = renovation;
            Start = start;
            End = end;
            Duration = duration;
            _dates = new ObservableCollection<DateTime>();
            NavigationClick = new RelayCommand(param => Navigation(param));
            service = new AccommodationRenovationService();
            Update();
        }
        private void Navigation(object parameter)
        {
            Renovation.FirstDay = SelectedRequest;
            Renovation.LastDay = SelectedRequest.AddDays(Duration);
            Renovation.Comment = Comment;
            service.Save(Renovation.toAccommodationRenovation());
        }
        private void Update()
        {
           var dates = service.FindAvailableRenovationDates(Start, End,Duration,Renovation.AccommodationId);
            _dates = new ObservableCollection<DateTime>(dates);
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
