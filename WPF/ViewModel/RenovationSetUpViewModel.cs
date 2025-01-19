using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.WPF.View.Owner;
using OxyPlot;
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
    public class RenovationSetUpViewModel: BindableBase
    {
        public User User { get; set; }
        public AccommodationRenovationDTO Reservation { get; set; }

        private AccommodationRenovationService service;

        public AccommodationDTO SelectedAccommodation { get; set; }
        public DateTime Start { get;set; }
        public DateTime End { get;set; }    
        public int Duration { get; set; }
        public ICommand NavigationClick { get; }
        public ICommand DeleteClick { get; }

        private ObservableCollection<AccommodationRenovationDTO> _renovations;
        public ObservableCollection<AccommodationRenovationDTO> Renovations
        {
            get { return _renovations; }
            set
            {
                if (_renovations != value)
                {
                    _renovations = value;
                    OnPropertyChanged(nameof(Renovations));
                }
            }
        }
        
        public int Id { get; set; } 
        
        private AccommodationRenovation _selectedRequest;
        public AccommodationRenovation SelectedRequest
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
        public RenovationSetUpViewModel(User user, AccommodationDTO selection)
        {
            User = user;
            SelectedAccommodation = selection;
            Reservation = new AccommodationRenovationDTO();
            Renovations = new ObservableCollection<AccommodationRenovationDTO>();
            NavigationClick = new RelayCommand(param => Navigation(param));
            DeleteClick = new RelayCommand(param => Delete(param));
            SelectedRequest = new AccommodationRenovation();
            service = new AccommodationRenovationService();
            Update();
        }
        private void Navigation(object parameter) 
        {
           RenovationFinal window = new RenovationFinal(Reservation, Start, End, Duration);
            window.ShowDialog();
        }
        private void Delete(object parameter)
        {
            SelectedRequest.Id = 1; 
            service.Delete(SelectedRequest);
            Update();
        }
        private void Update()
        {
            Reservation.AccommodationId = SelectedAccommodation.Id;
            foreach (var renovatino in service.GetAll()) 
            {
                _renovations.Add(renovatino.ToDTO(renovatino));
            }
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
