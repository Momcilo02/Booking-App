using BookingApp.Commands;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel
{
    public class RequestDetailsGuestViewModel : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }
        private string previousPeriod;
        public string PreviousPeriod
        {
            get { return previousPeriod; }
            set
            {
                if (previousPeriod != value)
                {
                    previousPeriod = value;
                    OnPropertyChanged(nameof(PreviousPeriod));
                }
            }
        }
        private string requestedPeriod;
        public string RequestedPeriod
        {
            get { return requestedPeriod; }
            set
            {
                if (requestedPeriod != value)
                {
                    requestedPeriod = value;
                    OnPropertyChanged(nameof(RequestedPeriod));
                }
            }
        }
        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }
        public ICommand CloseClick { get; }
        public RequestDetailsGuestViewModel(AccommodationReservationRequestDTO request)
        {
            Name = request.OldReservation.Accommodation.Name;
            PreviousPeriod = "Previous Period: From " + request.OldReservation.StartDate.ToString() + " To " + request.OldReservation.EndDate.ToString();
            Status = request.Status.ToString();
            RequestedPeriod = "Requested Period: From " + request.NewStartDate.ToString() + " To " + request.NewEndDate.ToString();
            if (request.Comment != null)
            {
                Comment = request.Comment;
            }
            else
            {
                Comment = "";
            }
            CloseClick = new RelayCommand(param => Close(param));

        }
        private void Close(object window) { 
            Window win = (Window)window;
            win.Close();
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
