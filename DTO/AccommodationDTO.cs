using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AccommodationDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set
            {
                if (value != locationId)
                {
                    locationId = value;
                    OnPropertyChanged(nameof(LocationId));
                }
            }
        }
        private string mergedLocation;
        public string MergedLocation
        {
            get { return mergedLocation; }
            set
            {
                if (value != mergedLocation)
                {
                    mergedLocation = value;
                    OnPropertyChanged(nameof(MergedLocation));
                }
            }
        }
        private Enumeration.AccommodationType type;
        public Enumeration.AccommodationType Type
        {
            get { return type; }
            set
            {
                if(value != type)
                {
                    type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }
        private int maxGuests;
        public int MaxGuests
        {
            get { return maxGuests; }
            set
            {
                if(value != maxGuests)
                {
                    maxGuests = value;
                    OnPropertyChanged(nameof(MaxGuests));
                }
            }
        }
        private int minReservationDays;
        public int MinReservationDays
        {
            get { return minReservationDays; }
            set
            {
                if(value!= minReservationDays)
                {
                    minReservationDays = value;
                    OnPropertyChanged(nameof(MinReservationDays));
                }
            }
        }
        private int uncancellablePeriod;
        public int UncancellablePeriod
        {
            get { return uncancellablePeriod; }
            set
            {
                if(value!=  uncancellablePeriod)
                {
                    uncancellablePeriod = value;
                    OnPropertyChanged(nameof(UncancellablePeriod));
                }
            }
        }
        private User owner;
        public User Owner 
        {
            get { return owner; }
            set 
            {
                if (value != owner)
                { 
                    owner = value;
                    OnPropertyChanged(nameof(Owner));
                }
            }
        }
        private string mainImagePath;
        public string? MainImagePath
        {
            get { return mainImagePath; }
            set    
            {
                if( mainImagePath != value)
                {
                    mainImagePath = value;
                    OnPropertyChanged(nameof(MainImagePath));
                }
            }
        }
        private List<Image> images;
        public List<Image> Images
        {
            get { return  images; }
            set { 
                if(images != value)
                {
                    images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }
        public AccommodationDTO() { }
        public AccommodationDTO(Accommodation accommodation)
        {
            Id = accommodation.Id;
            Name = accommodation.Name;
            MergedLocation = accommodation.Location.City + ", " + accommodation.Location.Country;
            LocationId = accommodation.Location.Id;
            MaxGuests = accommodation.MaxGuests;
            MinReservationDays = accommodation.MinReservationDays;
            UncancellablePeriod = accommodation.UncancellablePeriod;
            Owner = accommodation.Owner;
            Images = new List<Image>();
            MainImagePath = null;
        }
        public Accommodation ToAccommodation()
        {
            string[] locationParts = MergedLocation.Replace(" ", string.Empty).Split(",");
            
            return new Accommodation(Id, name, new Location(LocationId, locationParts[1], locationParts[0]), type,  maxGuests, minReservationDays, uncancellablePeriod,owner);
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
