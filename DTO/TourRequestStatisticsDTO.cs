using System.ComponentModel;

namespace BookingApp.DTO
{
    public class TourRequestStatisticsDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public TourRequestStatisticsDTO()
        {

        }

        private double aceptedTourByYearProcentage;
        public double AceptedTourByYearProcentage
        {
            get { return aceptedTourByYearProcentage; }
            set
            {
                if (value != aceptedTourByYearProcentage)
                {
                    aceptedTourByYearProcentage = value;
                    OnPropertyChanged(nameof(AceptedTourByYearProcentage));
                }
            }
        }

        private double declinedTourByYearProcentage;
        public double DeclinedTourByYearProcentage
        {
            get { return declinedTourByYearProcentage; }
            set
            {
                if (value != declinedTourByYearProcentage)
                {
                    declinedTourByYearProcentage = value;
                    OnPropertyChanged(nameof(DeclinedTourByYearProcentage));
                }
            }
        }

        private double aceptedTourProcentage;
        public double AceptedTourProcentage
        {
            get { return aceptedTourProcentage; }
            set
            {
                if (value != aceptedTourProcentage)
                {
                    aceptedTourProcentage = value;
                    OnPropertyChanged(nameof(AceptedTourProcentage));
                }
            }
        }

        private double declinedTourProcentage;
        public double DeclinedTourProcentage
        {
            get { return declinedTourProcentage; }
            set
            {
                if (value != declinedTourProcentage)
                {
                    declinedTourProcentage = value;
                    OnPropertyChanged(nameof(DeclinedTourProcentage));
                }
            }
        }

    }
}
