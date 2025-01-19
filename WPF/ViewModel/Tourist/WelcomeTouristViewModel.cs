using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.View.Tourist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class WelcomeTouristViewModel
    {
        public User User { get; set; }
        public ICommand YesClick { get; }
        public ICommand NoClick { get; }
        public Window _window;
        public WelcomeTouristViewModel(User user,Window window) { 
        
            _window = window;
            User = user;
            NoClick = new RelayCommand(param => No(param));
            YesClick = new RelayCommand(param => Yes(param));
        }
        private void No(object parameter)
        {
            TouristMainView touristMainView = new TouristMainView(User);
            touristMainView.ShowDialog();
            _window.Close();
        }
        private void Yes(object parameter)
        {
            TouristMainView touristMainView = new TouristMainView(User);
            touristMainView.ShowDialog();
            _window.Close();
        }
    }
}
