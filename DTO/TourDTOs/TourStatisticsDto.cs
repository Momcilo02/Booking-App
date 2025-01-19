using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO.TourDTOs
{
    public class TourStatisticsDto : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private DateTime _date;
        public DateTime Date 
        {
            get { return _date; }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _tourImage;
        public string TourImage
        {
            get { return _tourImage; }
            set
            {
                if (value != _tourImage)
                {
                    _tourImage = value;
                    OnPropertyChanged(nameof(TourImage));
                }
            }
        }


        private LocationDTO _locationDto;
        public LocationDTO LocationDto
        {
            get { return _locationDto; }
            set
            {
                if (value != _locationDto)
                {
                    _locationDto = value;
                    OnPropertyChanged(nameof(LocationDto));
                }
            }
        }

        private int  _totalNumerOfTourists;
        public int TotalNumerOfTourists
        {
            get { return _totalNumerOfTourists; }
            set
            {
                if (value != _totalNumerOfTourists)
                {
                    _totalNumerOfTourists = value;
                    OnPropertyChanged(nameof(TotalNumerOfTourists));
                }
            }
        }

        private int _under18Num;
        public int Under18Num
        {
            get { return _under18Num; }
            set
            {
                if (value != _under18Num)
                {
                    _under18Num = value;
                    OnPropertyChanged(nameof(Under18Num));
                }
            }
        }

        private int _from18To50;
        public int From18To50
        {
            get { return _from18To50; }
            set
            {
                if (value != _from18To50)
                {
                    _from18To50 = value;
                    OnPropertyChanged(nameof(From18To50));
                }
            }
        }

        private int _above50;
        public int Above50
        {
            get { return _above50; }
            set
            {
                if (value != _above50)
                {
                    _above50 = value;
                    OnPropertyChanged(nameof(Above50));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
