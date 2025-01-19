using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.DTO
{
    public class TouristNotificationDTO:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (value != title)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private List<string> description;
        public List<string> Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        private string _buttonName;
        public string ButtonName
        {
            get => _buttonName;
            set
            {
                if (_buttonName != value)
                {
                    _buttonName = value;
                    OnPropertyChanged(nameof(ButtonName));
                }
            }
        }
        public TouristNotificationDTO() { }
        public TouristNotificationDTO(TouristNotification touristNotification)
        {
            Id = touristNotification.Id;
            Title = touristNotification.Title;
            Description = touristNotification.Description;
            
        }
        public TouristNotification ToTouristNotification()
        {
            return new TouristNotification(Id,title, description);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
