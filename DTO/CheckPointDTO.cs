using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class CheckPointDTO: INotifyPropertyChanged
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

        private int order;
        public int Order
        {
            get { return order; }
            set
            {
                if (value != order)
                {
                    order = value;
                    OnPropertyChanged(nameof(Order));
                }
            }
        }

        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set
            {
                if (value != tourId)
                {
                    tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }


        public CheckPointDTO()
        {
            Name = string.Empty;
        }
        public CheckPointDTO(CheckPoint checkPoint)
        {
            Id = checkPoint.Id;
            Order=checkPoint.Order;
            Name = checkPoint.Name;
            TourId = checkPoint.TourId;
        }
        public CheckPoint ToCheckPoint()
        {
            return new CheckPoint(Id, order, name, tourId);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
